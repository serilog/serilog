using Serilog.Core;
using Serilog.Events;

namespace Serilog.Parameters
{
    class DepthLimiter : ILogEventPropertyValueFactory
    {
        const int MaximumDestructuringDepth = 10;

        readonly int _currentDepth;
        readonly PropertyValueConverter _propertyValueConverter;

        public DepthLimiter(int currentDepth, PropertyValueConverter propertyValueConverter)
        {
            _currentDepth = currentDepth;
            _propertyValueConverter = propertyValueConverter;
        }

        public LogEventPropertyValue CreatePropertyValue(object value, bool destructureObjects = false)
        {
            if (_currentDepth == MaximumDestructuringDepth)
                return new ScalarValue(null);

            return _propertyValueConverter.CreatePropertyValue(value, destructureObjects, _currentDepth + 1);
        }
    }
}
