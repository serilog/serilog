using System;
using System.Net.Http;
using Splunk.Client;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A specialised context relating to splunk that includes the authentication info
    /// </summary>
    public class SplunkContext : Context
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; private set; }

        public string Index { get;  private set ; }

        

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        public SplunkContext(Context context, string index, string username, string password) 
            :base(context.Scheme, context.Host, context.Port)
        {
            Index = index;
            Username = username;
            Password = password;
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
        public SplunkContext(Scheme scheme, string host, int port, string index, string username, string password, TimeSpan timeout, HttpMessageHandler handler, bool disposeHandler = true) : base(scheme, host, port, timeout, handler, disposeHandler)
        {
            Index = index;
            Username = username;
            Password = password;
        }
    }
}