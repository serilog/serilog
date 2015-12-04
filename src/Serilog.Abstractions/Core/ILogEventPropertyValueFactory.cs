using Serilog.Events;

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
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <returns>The value.</returns>
        LogEventPropertyValue CreatePropertyValue(object value, bool destructureObjects = false);
    }
}
