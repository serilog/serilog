using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    /// <summary>
    /// Determine how, when destructuring, a supplied 
    /// </summary>
    public interface IDestructuringPolicy
    {
        /// <summary>
        /// If supported, destructure the provided value.
        /// </summary>
        /// <param name="value">The value to destructure.</param>
        /// <param name="propertyValueFactory">Recursively apply policies to destructure additional values.</param>
        /// <param name="result">The destructured value, or null.</param>
        /// <returns>True if the value could be destructured under this policy.</returns>
        bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result);
    }
}
