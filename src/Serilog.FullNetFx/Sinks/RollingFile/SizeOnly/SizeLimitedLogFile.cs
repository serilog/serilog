namespace Serilog.Sinks.RollingFile.SizeOnly
{
    internal class SizeLimitedLogFile
    {
        public readonly long SizeLimitBytes;
        public readonly FileNameComponents FileNameComponents;

        public SizeLimitedLogFile(FileNameComponents fileNameComponents, long sizeLimitBytes)
        {
            FileNameComponents = fileNameComponents;
            SizeLimitBytes = sizeLimitBytes;
        }

        public string FullName { get { return FileNameComponents.FullName; } }
    }

    internal static class SizeLimitedLogFileExtensions
    {
        internal static SizeLimitedLogFile Next(this SizeLimitedLogFile previous)
        {
            var componentsIncremented = new FileNameComponents(previous.FileNameComponents.Name,
                previous.FileNameComponents.Sequence + 1, previous.FileNameComponents.Extension);
            return new SizeLimitedLogFile(
                componentsIncremented, previous.SizeLimitBytes);
        }
    }
}