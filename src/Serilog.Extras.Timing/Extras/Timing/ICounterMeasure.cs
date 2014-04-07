namespace Serilog.Extras.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICounterMeasure
    {
        /// <summary>
        /// Increments the counter
        /// </summary>
        void Increment();

        /// <summary>
        /// Decrements the counter
        /// </summary>
        void Decrement();

        /// <summary>
        /// Resets the counter back to zero
        /// </summary>
        void Reset();
    }
}