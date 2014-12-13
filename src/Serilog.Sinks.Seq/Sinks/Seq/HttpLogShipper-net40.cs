using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Sinks.Seq
{
    class HttpLogShipper : IDisposable
    {
        readonly string _apiKey;
        readonly int _batchPostingLimit;
        readonly Timer _timer;
        readonly TimeSpan _period;
        readonly object _stateLock = new object();
        LogEventLevel? _minimumAcceptedLevel;
        volatile bool _unloading;
        readonly string _bookmarkFilename;
        readonly string _logFolder;
        readonly HttpClient _httpClient;
        readonly string _candidateSearchPath;

        const string ApiKeyHeaderName = "X-Seq-ApiKey";
        const string BulkUploadResource = "api/events/raw";

        public HttpLogShipper(string serverUrl, string bufferBaseFilename, string apiKey, int batchPostingLimit, TimeSpan period)
        {
            _apiKey = apiKey;
            _batchPostingLimit = batchPostingLimit;
            _period = period;

            var baseUri = serverUrl;
            if (!baseUri.EndsWith("/"))
                baseUri += "/";

            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };

            _bookmarkFilename = Path.GetFullPath(bufferBaseFilename + ".bookmark");
            _logFolder = Path.GetDirectoryName(_bookmarkFilename);
            _candidateSearchPath = Path.GetFileName(bufferBaseFilename) + "*.json";
            _timer = new Timer(s => OnTick());
            _period = period;

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
        /// Get the last "minimum level" indicated by the Seq server, if any.
        /// </summary>
        public LogEventLevel? MinimumAcceptedLevel
        {
            get
            {
                lock (_stateLock)
                    return _minimumAcceptedLevel;
            }
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

            _timer.Change(_period, TimeSpan.FromDays(30));
        }

        void OnTick()
        {
            LogEventLevel? minimumAcceptedLevel = null;

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
                        string currentFile;

                        TryReadBookmark(bookmark, out nextLineBeginsAtOffset, out currentFile);

                        var fileSet = GetFileSet();

                        if (currentFile == null || !File.Exists(currentFile))
                        {
                            nextLineBeginsAtOffset = 0;
                            currentFile = fileSet.FirstOrDefault();
                        }

                        if (currentFile != null)
                        {
                            var payload = new StringWriter();
                            payload.Write("{\"events\":[");
                            count = 0;
                            var delimStart = "";

                            using (var current = File.Open(currentFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                current.Position = nextLineBeginsAtOffset;

                                string nextLine;
                                while (count < _batchPostingLimit &&
                                    TryReadLine(current, ref nextLineBeginsAtOffset, out nextLine))
                                {
                                    ++count;
                                    payload.Write(delimStart);
                                    payload.Write(nextLine);
                                    delimStart = ",";
                                }

                                payload.Write("]}");
                            }

                            if (count > 0)
                            {
                                var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                                if (!string.IsNullOrWhiteSpace(_apiKey))
                                    content.Headers.Add(ApiKeyHeaderName, _apiKey);

                                var result = _httpClient.PostAsync(BulkUploadResource, content).Result;
                                if (result.IsSuccessStatusCode)
                                {
                                    WriteBookmark(bookmark, nextLineBeginsAtOffset, currentFile);
                                    var returned = result.Content.ReadAsStringAsync().Result;
                                    minimumAcceptedLevel = SeqApi.ReadEventInputResult(returned);
                                }
                                else
                                {
                                    SelfLog.WriteLine("Received failed HTTP shipping result {0}: {1}", result.StatusCode, result.Content.ReadAsStringAsync().Result);
                                }
                            }
                            else
                            {
                                // Only advance the bookmark if no other process has the
                                // current file locked, and its length is as we found it.
                                
                                if (fileSet.Length == 2 && fileSet.First() == currentFile && IsUnlockedAtLength(currentFile, nextLineBeginsAtOffset))
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
                    _minimumAcceptedLevel = minimumAcceptedLevel;
                    if (!_unloading)
                        SetTimer();
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

        static void WriteBookmark(Stream bookmark, long nextLineBeginsAtOffset, string currentFile)
        {
            // Important not to dispose this StreamWriter as the underlying stream must stay open.
            var writer = new StreamWriter(bookmark);
            writer.WriteLine("{0}:::{1}", nextLineBeginsAtOffset, currentFile);
            writer.Flush();
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

            // Important not to dispose this StreamReader as the stream must remain open.
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
                // Important not to dispose this StreamReader as the stream must remain open.
                var reader = new StreamReader(bookmark, Encoding.UTF8, false, 128);
                var current = reader.ReadLine();

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