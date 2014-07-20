//using System;
//using System.Text;
//using System.Threading.Tasks;
//using Serilog.Core;
//using Serilog.Events;



//namespace Serilog.Sinks.Splunk
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class SplunkViaUdpSink :ILogEventSink
//    {
//        readonly IFormatProvider _formatProvider;

//        System.Net _socket;


//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="host"></param>
//        /// <param name="port"></param>
//        /// <param name="batchSizeLimit"></param>
//        /// <param name="period"></param>
//        /// <param name="formatProvider"></param>
//        public SplunkViaUdpSink(
//            IPAddress host,
//            int port,
//            int batchSizeLimit,
//            TimeSpan period,
//            IFormatProvider formatProvider = null)
//            : base(batchSizeLimit, period)
//        {
//            _formatProvider = formatProvider;
//            System.Net.Sockets.Dgram
//            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
//            _socket.Connect(host, port);
//            // _listener = new UdpTraceListener(host, port);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="host"></param>
//        /// <param name="port"></param>
//        /// <param name="batchSizeLimit"></param>
//        /// <param name="period"></param>
//        /// <param name="formatProvider"></param>
//        public SplunkViaUdpSink(
//            string host,
//            int port,
//            int batchSizeLimit,
//            TimeSpan period,
//            IFormatProvider formatProvider = null)
//            : base(batchSizeLimit, period)
//        {

//            _formatProvider = formatProvider;

//            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

//            _socket.Connect(host, port);

//            //  _listener = new UdpTraceListener(host, port);
//        }
       

//        public void Emit(LogEvent logEvent)
//        {
            


//            //TODO: Change to connect on emit
//            //TODO: Change to async?

//             Task.Factory.StartNew(() =>
//                {
                    

//                        var message = _formatProvider != null
//                            ? logEvent.RenderMessage(_formatProvider)
//                            : logEvent.RenderMessage();

//                        _socket.Send(Encoding.UTF8.GetBytes(message));
                    


//                });
//        }
//    }
//}