using System;

namespace Serilog.Core
{
    /// <summary>
    /// Indicates that the marked method logs data using a message template and (optional) arguments.
    /// The name of the parameter which contains the message template should be given in the constructor.
    /// </summary>
    /// <example>
    /// <code>
    /// [LoggerMethod("messageTemplate")]
    /// public void Information(string messageTemplate, params object[] propertyValues)
    /// {
    ///     // Do something
    /// }
    ///
    /// public void Foo()
    /// {
    ///     Information("Hello, {Name}!") // Warning: Non-existing argument in message template.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public sealed class MessageTemplateFormatMethodAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplateFormatMethodAttribute"/> class.
        /// </summary>
        /// <param name="messageTemplateParameterName">Name of the message template parameter.</param>
        public MessageTemplateFormatMethodAttribute(string messageTemplateParameterName)
        {
            MessageTemplateParameterName = messageTemplateParameterName;
        }

        /// <summary>
        /// Gets the name of the message template parameter.
        /// </summary>
        /// <value>The name of the message template parameter.</value>
        public string MessageTemplateParameterName { get; private set; }
    }
}