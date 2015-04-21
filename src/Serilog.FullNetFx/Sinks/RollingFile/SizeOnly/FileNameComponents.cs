namespace Serilog.Sinks.RollingFile.SizeOnly
{
    internal class FileNameComponents
    {
        internal readonly string Name;
        internal readonly uint Sequence;
        internal readonly string Extension;
        internal readonly string FullName;
        private const string FullNameFormat = "{0}{1}.{2}";
        private const string FullNameNoExtension = "{0}{1}";
        private const string NumberFormat = "00000";

        public FileNameComponents(string name, uint sequence, string extension)
        {
            Name = name;
            Sequence = sequence;
            Extension = extension;
            FullName = string.IsNullOrWhiteSpace(extension)
                ? string.Format(FullNameNoExtension, name, sequence.ToString(NumberFormat))
                : string.Format(FullNameFormat, name, sequence.ToString(NumberFormat), extension);
        }
    }
}