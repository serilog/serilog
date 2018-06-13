using System;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyHardCodedStringDestructuringPolicy : IDestructuringPolicy
    {
        readonly string _hardCodedString;

        public DummyHardCodedStringDestructuringPolicy(string hardCodedString)
        {
            _hardCodedString = hardCodedString ?? throw new ArgumentNullException(nameof(hardCodedString));
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            result = new ScalarValue(_hardCodedString);
            return true;
        }
    }
}
