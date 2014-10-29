using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

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
            var startInfo = new ProcessStartInfo
                                {
                                    WindowStyle = ProcessWindowStyle.Normal,
                                    ErrorDialog = true,
                                    LoadUserProfile = true,
                                    CreateNoWindow = false,
                                    UseShellExecute = false,
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
                    _eventstoreprocess.CloseMainWindow();
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
