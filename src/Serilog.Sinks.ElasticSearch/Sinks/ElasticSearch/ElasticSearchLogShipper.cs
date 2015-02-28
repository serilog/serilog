// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using Serilog.Debugging;

namespace Serilog.Sinks.ElasticSearch
{
    class ElasticSearchLogShipper : IDisposable
    {
        readonly ElasticsearchClient _client;

        readonly int _batchPostingLimit;
        readonly Timer _timer;
        readonly TimeSpan _period;
        readonly object _stateLock = new object();
        volatile bool _unloading;
        readonly string _bookmarkFilename;
        readonly string _logFolder;
        readonly string _candidateSearchPath;
        readonly string _typeName;
        readonly string _indexFormat;

        public ElasticSearchLogShipper(ElasticsearchSinkOptions options)
        {
            var configuration = new ConnectionConfiguration(options.ConnectionPool)
                .SetTimeout(ElasticsearchSink.DefaultConnectionTimeout)
                .SetMaximumAsyncConnections(20);

            _period = options.BufferLogShippingInterval ?? TimeSpan.FromSeconds(5);

            _indexFormat = !string.IsNullOrWhiteSpace(options.IndexFormat) ? options.IndexFormat : ElasticsearchSink.DefaultIndexFormat;
            _typeName = !string.IsNullOrWhiteSpace(options.TypeName) ? options.TypeName : ElasticsearchSink.DefaultTypeName;

            _client = new ElasticsearchClient(configuration, connection: options.Connection, serializer: options.Serializer);

            _batchPostingLimit = options.BatchPostingLimit ?? 100;

            _bookmarkFilename = Path.GetFullPath(options.BufferBaseFilename + ".bookmark");
            _logFolder = Path.GetDirectoryName(_bookmarkFilename);
            _candidateSearchPath = Path.GetFileName(options.BufferBaseFilename) + "*.json";

            _timer = new Timer(s => OnTick());

            AppDomain.CurrentDomain.DomainUnload += OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit += OnAppDomainUnloading;

            SetTimer();
        }

        void OnAppDomainUnloading(object sender, EventArgs args)
        {
            CloseAndFlush();
        }

        void CloseAndFlush()
        {
            lock (_stateLock)
            {
                if (_unloading)
                    return;

                _unloading = true;
            }

            AppDomain.CurrentDomain.DomainUnload -= OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit -= OnAppDomainUnloading;

            var wh = new ManualResetEvent(false);
            if (_timer.Dispose(wh))
                wh.WaitOne();

            OnTick();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Free resources held by the sink.
        /// </summary>
        /// <param name="disposing">If true, called because the object is being disposed; if false,
        /// the object is being disposed from the finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            CloseAndFlush();
        }

        void SetTimer()
        {
            // Note, called under _stateLock
            var infiniteTimespan = TimeSpan.FromMilliseconds(Timeout.Infinite); //< can't use Timeout.InfiniteTimespan in .NET 4
            
            _timer.Change(_period, infiniteTimespan);
        }

        void OnTick()
        {
            try
            {
                var count = 0;

                do
                {
                    // Locking the bookmark ensures that though there may be multiple instances of this
                    // class running, only one will ship logs at a time.

                    using (var bookmark = File.Open(_bookmarkFilename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
                    {
                        long nextLineBeginsAtOffset;
                        string currentFilePath;

                        TryReadBookmark(bookmark, out nextLineBeginsAtOffset, out currentFilePath);

                        var fileSet = GetFileSet();

                        if (currentFilePath == null || !File.Exists(currentFilePath))
                        {
                            nextLineBeginsAtOffset = 0;
                            currentFilePath = fileSet.FirstOrDefault();
                        }

                        if (currentFilePath == null) continue;

                        count = 0;

                        // file name pattern: whatever-bla-bla-20150218.json, whatever-bla-bla-20150218_1.json, etc.
                        var lastToken = currentFilePath.Split('-').Last();

                        // lastToken should be something like 20150218.json or 20150218_3.json now
                        if (!lastToken.ToLowerInvariant().EndsWith(".json"))
                        {
                            throw new FormatException(string.Format("The file name '{0}' does not seem to follow the right file pattern - it must be named [whatever]-{{Date}}[_n].json", Path.GetFileName(currentFilePath)));
                        }

                        var dateString = lastToken.Substring(0, 8);
                        var date = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);

                        var payload = new List<string>();

                        using (var current = File.Open(currentFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            current.Position = nextLineBeginsAtOffset;

                            string nextLine;
                            while (count < _batchPostingLimit && TryReadLine(current, ref nextLineBeginsAtOffset, out nextLine))
                            {
                                var indexName = string.Format(_indexFormat, date);
                                var action = new { index = new { _index = indexName, _type = _typeName } };
                                var actionJson = _client.Serializer.Serialize(action, SerializationFormatting.None);

                                payload.Add(Encoding.UTF8.GetString(actionJson));
                                payload.Add(nextLine);
                                ++count;
                            }
                        }

                        if (count > 0)
                        {
                            var response = _client.Bulk(payload);

                            if (response.Success)
                            {
                                WriteBookmark(bookmark, nextLineBeginsAtOffset, currentFilePath);
                            }
                            else
                            {
                                SelfLog.WriteLine("Received failed ElasticSearch shipping result {0}: {1}", response.HttpStatusCode, response.OriginalException);
                            }
                        }
                        else
                        {
                            // Only advance the bookmark if no other process has the
                            // current file locked, and its length is as we found it.

                            if (fileSet.Length == 2 && fileSet.First() == currentFilePath && IsUnlockedAtLength(currentFilePath, nextLineBeginsAtOffset))
                            {
                                WriteBookmark(bookmark, 0, fileSet[1]);
                            }

                            if (fileSet.Length > 2)
                            {
                                // Once there's a third file waiting to ship, we do our
                                // best to move on, though a lock on the current file
                                // will delay this.

                                File.Delete(fileSet[0]);
                            }
                        }
                    }
                }
                while (count == _batchPostingLimit);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Exception while emitting periodic batch from {0}: {1}", this, ex);
            }
            finally
            {
                lock (_stateLock)
                {
                    if (!_unloading)
                    {
                        SetTimer();
                    }
                }
            }
        }

        bool IsUnlockedAtLength(string file, long maxLen)
        {
            try
            {
                using (var fileStream = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
                {
                    return fileStream.Length <= maxLen;
                }
            }
            catch (IOException ex)
            {
                var errorCode = Marshal.GetHRForException(ex) & ((1 << 16) - 1);
                if (errorCode != 32 && errorCode != 33)
                {
                    SelfLog.WriteLine("Unexpected I/O exception while testing locked status of {0}: {1}", file, ex);
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Unexpected exception while testing locked status of {0}: {1}", file, ex);
            }

            return false;
        }

        static void WriteBookmark(FileStream bookmark, long nextLineBeginsAtOffset, string currentFile)
        {
            using (var writer = new StreamWriter(bookmark))
            {
                writer.WriteLine("{0}:::{1}", nextLineBeginsAtOffset, currentFile);
            }
        }

        // It would be ideal to chomp whitespace here, but not required.
        static bool TryReadLine(Stream current, ref long nextStart, out string nextLine)
        {
            var includesBom = nextStart == 0;

            if (current.Length <= nextStart)
            {
                nextLine = null;
                return false;
            }

            current.Position = nextStart;

            // Important not to dispose this StreamReader as the stream must remain open (and we can't use the overload with 'leaveOpen' as it's not available in .NET4
            var reader = new StreamReader(current, Encoding.UTF8, false, 128);
            nextLine = reader.ReadLine();

            if (nextLine == null)
                return false;

            nextStart += Encoding.UTF8.GetByteCount(nextLine) + Encoding.UTF8.GetByteCount(Environment.NewLine);
            if (includesBom)
                nextStart += 3;

            return true;
        }

        static void TryReadBookmark(Stream bookmark, out long nextLineBeginsAtOffset, out string currentFile)
        {
            nextLineBeginsAtOffset = 0;
            currentFile = null;

            if (bookmark.Length != 0)
            {
                string current;

                // Important not to dispose this StreamReader as the stream must remain open (and we can't use the overload with 'leaveOpen' as it's not available in .NET4
                var reader = new StreamReader(bookmark, Encoding.UTF8, false, 128);
                current = reader.ReadLine();

                if (current != null)
                {
                    bookmark.Position = 0;
                    var parts = current.Split(new[] { ":::" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        nextLineBeginsAtOffset = long.Parse(parts[0]);
                        currentFile = parts[1];
                    }
                }

            }
        }

        string[] GetFileSet()
        {
            return Directory.GetFiles(_logFolder, _candidateSearchPath)
                .OrderBy(n => n)
                .ToArray();
        }
    }
}