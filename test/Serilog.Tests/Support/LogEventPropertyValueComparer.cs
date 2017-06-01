using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;

namespace Serilog.Tests.Support
{
    class LogEventPropertyValueComparer : IEqualityComparer<LogEventPropertyValue>
    {
        readonly IEqualityComparer<object> _objectEqualityComparer;

        public LogEventPropertyValueComparer(IEqualityComparer<object> objectEqualityComparer = null)
        {
            _objectEqualityComparer = objectEqualityComparer ?? EqualityComparer<object>.Default;
        }

        public bool Equals(LogEventPropertyValue x, LogEventPropertyValue y)
        {
            var scalarX = x as ScalarValue;
            var scalarY = y as ScalarValue;
            if (scalarX != null && scalarY != null)
            {
                return _objectEqualityComparer.Equals(scalarX.Value, scalarY.Value);
            }

            var sequenceX = x as SequenceValue;
            var sequenceY = y as SequenceValue;
            if (sequenceX != null && sequenceY != null)
            {
                return sequenceX.Elements
                    .SequenceEqual(sequenceY.Elements, this);
            }

            if (x is StructureValue || y is StructureValue)
            {
                throw new NotImplementedException();
            }

            if (x is DictionaryValue || y is DictionaryValue)
            {
                throw new NotImplementedException();
            }

            return false;
        }

        public int GetHashCode(LogEventPropertyValue obj)
        {
            return 0;
        }
    }
}
