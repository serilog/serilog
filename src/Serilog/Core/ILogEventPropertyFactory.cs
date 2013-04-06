using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Creates log event properties from regular .NET objects, applying policies as
    /// required.
    /// </summary>
    public interface ILogEventPropertyFactory
    {
        /// <summary>
        /// Construct a <see cref="LogEventProperty"/> with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="destructureObjects">If true, and the value is a non-primitive, non-array type,
        /// then the value will be converted to a structure; otherwise, unknown types will
        /// be converted to scalars, which are generally stored as strings.</param>
        /// <returns></returns>
        LogEventProperty CreateProperty(string name, object value, bool destructureObjects = false);
    }
}