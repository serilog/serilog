using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    /// <summary>
    /// Supports the policy-driven construction of <see cref="LogEventPropertyValue"/>s given
    /// regular .NET objects.
    /// </summary>
    public interface ILogEventPropertyValueFactory
    {
        /// <summary>
        /// Create a <see cref="LogEventPropertyValue"/> given a .NET object and destructuring
        /// strategy.
        /// </summary>
        /// <param name="value">The value of the property.</param>
        /// <param name="destructuring">Directs the algorithm converting the value.</param>
        /// <returns>The value.</returns>
        LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring);
    }
}
