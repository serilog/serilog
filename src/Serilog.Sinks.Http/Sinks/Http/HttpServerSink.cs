using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using Newtonsoft.Json;
using Serilog.Core;
using Seriline.Contracts;
using Serilog.Events;

namespace Serilog.Sinks.Http
{
    class HttpServerSink : ILogEventSink
    {
        readonly ConcurrentQueue<LogEvent> _queue;
        readonly Timer _timer;
        readonly HttpClient _httpClient;

        const int MaxPost = 50;     // "
        readonly TimeSpan TickInterval = TimeSpan.FromSeconds(2);
        const string EventStreamResource = "eventstream";

        public HttpServerSink(string baseAddress)
        {
            _queue = new ConcurrentQueue<LogEvent>();
            _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _timer = new Timer(OnTick);
            SetTimer();
        }

        void SetTimer()
        {
            _timer.Change(TickInterval, TimeSpan.FromMilliseconds(-1));
        }

        async void OnTick(object state)
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

            var series = new Series { Events = events.Select(ToEvent).ToArray() };

            try
            {
                await _httpClient.PostAsync(EventStreamResource, series, new JsonMediaTypeFormatter
                {
                    SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple }
                });
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
            finally
            {      
                SetTimer();
            }
        }

        static Event ToEvent(LogEvent logEvent)
        {
            return new Event
            {
                TimeStamp = logEvent.TimeStamp,
                Properties = logEvent.Properties.Values.Select(ToEventProperty).ToArray()
            };
        }

        static EventProperty ToEventProperty(LogEventProperty logEventProperty)
        {
            return new EventStringProperty { Name = logEventProperty.Name, Value = "" };
        }

        public void Write(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            _queue.Enqueue(logEvent);
        }
    }
}
