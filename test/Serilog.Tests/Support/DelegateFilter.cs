using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class DelegateFilter : ILogEventFilter
    {
        readonly Func<LogEvent, bool> _predicate;

        public DelegateFilter(Func<LogEvent, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException("predicate");
            _predicate = predicate;
        }

        public bool IsEnabled(LogEvent logEvent)
        {
            return _predicate(logEvent);
        }
    }
}
