using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Serilog.Events;

namespace Serilog.Extras.MSOwin
{
    public class SerilogMiddlewareTests
    {
        [TestFixture]
        public class WhenUsingARequestContext
        {
            LogEvent _eventSeen;
            readonly TestServer _server;

            public WhenUsingARequestContext()
            {
                var logger = new LoggerConfiguration()
                    .WriteTo
                    .Observers(events => events
                        .Do(evt => { _eventSeen = evt; })
                        .Subscribe())
                    .Enrich
                    .FromLogContext()
                    .CreateLogger();
                Log.Logger = logger;

                _server = TestServer.Create(
                    app => app.UseSerilogRequestContext()
                        .Use((context, func) =>
                        {
                            Log.Information("message");
                            return Task.Delay(0);
                        }));
            }

            [Test]
            public async Task Should_have_request_id_in_logevent_properties()
            {
                await MakeRequest();

                Assert.True(_eventSeen.Properties.ContainsKey(RequestContextMiddleware.DefaultRequestIdPropertyName));
            }

            [Test]
            public async Task Request_id_should_be_a_guid()
            {
                await MakeRequest();

                Guid _;
                Assert.True(Guid.TryParse(_eventSeen.Properties[RequestContextMiddleware.DefaultRequestIdPropertyName].ToString(), out _));
            }

            async Task MakeRequest()
            {
                await _server.CreateRequest("/").GetAsync();
            }
        }
    }
}