using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class LogEventPropertyStructuralEqualityComparer : IEqualityComparer<LogEventProperty>
    {
        readonly IEqualityComparer<LogEventPropertyValue> _valueEqualityComparer;

        public LogEventPropertyStructuralEqualityComparer(
            IEqualityComparer<LogEventPropertyValue> valueEqualityComparer = null)
        {
            this._valueEqualityComparer =
                valueEqualityComparer ?? new LogEventPropertyValueComparer(EqualityComparer<object>.Default);
        }

        public bool Equals(LogEventProperty x, LogEventProperty y)
        {
            return x.Name == y.Name
                   && _valueEqualityComparer.Equals(x.Value, y.Value);
        }

        public int GetHashCode(LogEventProperty obj)
        {
            return 0;
        }
    }
}
