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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Serilog.Sinks.EventStore.Tests
{
    /// <summary>
    /// A class to run and dispose the EventStore executable.
    /// </summary>
    internal class EventStoreRunner: IDisposable
    {
        bool _disposed;

        Process _eventstoreprocess;
        public EventStoreRunner()
        {
            var thread = new Thread(StartEventStore)
                             {
                                 IsBackground = true
                             };
        thread.Start();
        }

        private void StartEventStore()
        {
            var eventStoreFolder = Assembly.GetExecutingAssembly().GetExecutingFolder();
            int testStart = eventStoreFolder.IndexOf("test", StringComparison.OrdinalIgnoreCase);
            if (testStart == -1)
            {
                throw new InvalidOperationException("The executing folder does not conform to the expected format.");
            }
            eventStoreFolder =eventStoreFolder.Remove(testStart);
            eventStoreFolder = Path.Combine(eventStoreFolder, "EventStore Binaries", "EventStore.ClusterNode.exe");
            var startInfo = new ProcessStartInfo
                                {
                                    WindowStyle = ProcessWindowStyle.Normal,
                                    ErrorDialog = true,
                                    LoadUserProfile = true,
                                    CreateNoWindow = false,
                                    UseShellExecute = false,
                                FileName =eventStoreFolder 
                                };

            _eventstoreprocess = new Process
                                     {
                                         StartInfo = startInfo
                                     };
            
            _eventstoreprocess.Start();
            _eventstoreprocess.WaitForExit();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _eventstoreprocess.Kill();
                    _eventstoreprocess.Dispose();
                    _disposed = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
