using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Policies
{
    class NullableDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, Destructuring destructuring, ILogEventPropertyValueFactory propertyValueFactory,
                                   out LogEventPropertyValue result)
        {
            var type = value.GetType();
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                result = null;
                return false;
            }

            var dynamicValue = (dynamic)value;
            result = propertyValueFactory.CreatePropertyValue(
                dynamicValue.HasValue ? dynamicValue.Value : null, destructuring);

            return true;
        }
    }
}
