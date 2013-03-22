using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.CouchDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class CouchDBSink : ILogEventSink, IDisposable
    {
        readonly IMessageTemplateCache _parsedTemplateCache;
        readonly ConcurrentQueue<LogEvent> _queue;
        readonly Timer _timer;
        readonly HttpClient _httpClient;

        const int MaxPost = 50;     // "
        readonly TimeSpan TickInterval = TimeSpan.FromSeconds(2);
        const string BulkUploadResource = "_bulk_docs";

        volatile bool _unloading;

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a CouchDB database.</param>
        /// <param name="parsedTemplateCache">Cache for parsed templates.</param>
        public CouchDBSink(string databaseUrl, IMessageTemplateCache parsedTemplateCache)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            if (parsedTemplateCache == null) throw new ArgumentNullException("parsedTemplateCache");
            _parsedTemplateCache = parsedTemplateCache;
            _queue = new ConcurrentQueue<LogEvent>();
            _httpClient = new HttpClient { BaseAddress = new Uri(databaseUrl) };
            _timer = new Timer(async s => await OnTick());

            AppDomain.CurrentDomain.DomainUnload += OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit += OnAppDomainUnloading;
            SetTimer();
        }

        void OnAppDomainUnloading(object sender, EventArgs args)
        {
            _unloading = true;
            OnTick().Wait();
        }

        void SetTimer()
        {
            _timer.Change(TickInterval, TimeSpan.FromMilliseconds(-1));
        }

        async Task OnTick()
        {
            var count = 0;
            var events = new Queue<LogEvent>();
            LogEvent next;
            while (count < MaxPost && _queue.TryDequeue(out next))
            {
                count++;
                events.Enqueue(next);
            }

            if (events.Count == 0)
                return;

            var payload = new StringWriter();
            payload.Write("{\"docs\":[");

            var formatter = new SimpleJsonFormatter(true);
            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                var renderedMessage = _parsedTemplateCache.GetParsedTemplate(logEvent.MessageTemplate).Render(logEvent.Properties);
                payload.Write(",\"UtcTimeStamp\":\"{0:u}\",\"RenderedMessage\":\"{1}\"}}",
                    logEvent.TimeStamp.ToUniversalTime().DateTime,
                    renderedMessage);
                delimStart = ",{";
            }

            payload.Write("]}");

            try
            {
                var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync(BulkUploadResource, content);
                if (!result.IsSuccessStatusCode)
                    (SelfLog.Out ?? new StringWriter()).Write("Received failed result {0}: {1}", result.StatusCode, result.Content.ReadAsStringAsync().Result);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
            finally
            {
                if (!_unloading)
                    SetTimer();
            }
        }

        /// <summary>
        /// Write the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">Log event to write.</param>
        /// <exception cref="ArgumentNullException">The event is null.</exception>
        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (!_unloading)
                _queue.Enqueue(logEvent);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            AppDomain.CurrentDomain.DomainUnload -= OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit -= OnAppDomainUnloading;
        }
    }
}
