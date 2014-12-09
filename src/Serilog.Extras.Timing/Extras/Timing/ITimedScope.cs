using System;

namespace Serilog.Extras.Timing
{
    /// <summary>
    /// Disposable scope for the timing. Supports explicit completetion messages.
    /// </summary>
    public interface ITimedScope : IDisposable
    {
        /// <summary>
        /// Sets an alternative completetion message.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        void SetCompletetionMessage(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Sets an alternative completetion message and sets the log level to Error.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        void SetErrorMessage(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Sets an alternative completetion message and sets the log level to Warning.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        void SetWarningMessage(string messageTemplate, params object[] propertyValues);
    }
}