using System;

namespace Serilog.Settings
{
    /// <summary>
    /// Indicates that the marked assembly depends on the type that is specified in the constructor.
    /// Typically used to force a compile-time dependency to the assembly that contains the type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ConfigurationDependencyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDependencyAttribute"/> class.
        /// </summary>
        /// <param name="dependencyType">A type from the assembly that is used dynamically.</param>
        public ConfigurationDependencyAttribute(Type dependencyType)
        {
            DependencyType = dependencyType;
        }

        /// <summary>
        /// Gets the dependent type reference.
        /// </summary>
        /// <value>The dependent type reference.</value>
        public Type DependencyType { get; private set; }
    }
}
