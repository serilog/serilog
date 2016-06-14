using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class LogEventPropertyValueComparer : IEqualityComparer<LogEventPropertyValue>
    {
        readonly IEqualityComparer<object> _objectEqualityComparer;

        public LogEventPropertyValueComparer(IEqualityComparer<object> objectEqualityComparer = null)
        {
            this._objectEqualityComparer = objectEqualityComparer ?? EqualityComparer<object>.Default;
        }

        public bool Equals(LogEventPropertyValue x, LogEventPropertyValue y)
        {
            var scalarX = x as ScalarValue;
            var scalarY = y as ScalarValue;
            if (scalarX != null && scalarY != null)
            {
                return _objectEqualityComparer.Equals(scalarX.Value, scalarY.Value);
            }
            else if (x is SequenceValue && y is SequenceValue)
            {
                throw new NotImplementedException();
            }
            else if (x is StructureValue && y is StructureValue)
            {
                throw new NotImplementedException();
            }
            else if (x is DictionaryValue && y is DictionaryValue)
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        public int GetHashCode(LogEventPropertyValue obj)
        {
            return 0;
        }
    }
}