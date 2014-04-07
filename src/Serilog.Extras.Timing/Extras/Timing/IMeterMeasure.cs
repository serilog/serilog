namespace Serilog.Extras.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMeterMeasure
    {
        /// <summary>
        /// Marks the occurrence of an operation.
        /// </summary>
        void Mark(long n =1);
    }
}