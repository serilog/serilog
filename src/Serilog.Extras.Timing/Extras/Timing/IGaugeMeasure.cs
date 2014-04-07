namespace Serilog.Extras.Timing
{
    /// <summary>
    /// Measures an operation.
    /// </summary>
    public interface IGaugeMeasure
    {
        /// <summary>
        /// Performs the measurement
        /// </summary>
        void Measure();
    }
}