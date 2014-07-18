using System;
using System.Dynamic;
using System.Net.Http;
using Splunk.Client;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A specialised context relating to splunk that includes the authentication info
    /// </summary>
    public class SplunkContext : global::Splunk.Client.Context
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        /// <param name="scheme">The scheme to use (http or https)</param>
        /// <param name="host">The host of the splunk intance</param>
        /// <param name="port">The management port of the splunk instance</param>
        /// <param name="timeout">The timeout for the calls to SplunkViaHttp</param>
        public SplunkContext(global::Splunk.Client.Scheme scheme, string host, int port, TimeSpan timeout)
            : base(scheme, host, port, timeout)
        {
        }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        public SplunkContext(Context context, string username, string password) 
            :base(context.Scheme, context.Host, context.Port)
        {
        }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        /// <param name="scheme">The scheme to use (http or https)</param>
        /// <param name="host">The host of the splunk intance</param>
        /// <param name="port">The management port of the splunk instance</param> 
        public SplunkContext(global::Splunk.Client.Scheme scheme, string host, int port)
            : base(scheme, host, port)
        {
        }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="timeout"></param>
        /// <param name="handler"></param>
        /// <param name="disposeHandler"></param>
        public SplunkContext(global::Splunk.Client.Scheme scheme, string host, int port, TimeSpan timeout, HttpMessageHandler handler, bool disposeHandler = true) : base(scheme, host, port, timeout, handler, disposeHandler)
        {
        }
    }
}