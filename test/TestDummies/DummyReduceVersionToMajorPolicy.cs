using System;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyReduceVersionToMajorPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value is Version version)
            {
                result = new ScalarValue(version.Major);
                return true;
            }

            result = null;
            return false;
        }
    }
}
