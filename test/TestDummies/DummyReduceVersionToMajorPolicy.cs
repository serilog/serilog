using System;
using System.Diagnostics.CodeAnalysis;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies;

public class DummyReduceVersionToMajorPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
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
