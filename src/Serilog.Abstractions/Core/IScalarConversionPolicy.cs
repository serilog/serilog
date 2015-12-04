using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Determine how a simple value is carried through the logging
    /// pipeline as an immutable <see cref="ScalarValue"/>.
    /// </summary>
    public interface IScalarConversionPolicy
    {
        /// <summary>
        /// If supported, convert the provided value into an immutable scalar.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="propertyValueFactory">Recursively apply policies to convert additional values.</param>
        /// <param name="result">The converted value, or null.</param>
        /// <returns>True if the value could be converted under this policy.</returns>
        bool TryConvertToScalar(object value, ILogEventPropertyValueFactory propertyValueFactory, out ScalarValue result);
    }
}