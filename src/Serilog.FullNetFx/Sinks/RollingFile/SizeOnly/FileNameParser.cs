using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Serilog.Sinks.RollingFile.SizeOnly
{
    internal static class FileNameParser
    {
        internal static Regex LogFileFormat = new Regex(@"(?<name>\S+)(?<digits>\d{5})|(?<name>\S+)");
        private const string CannotParseLogFileForFilenameExceptionFormat =
            "Cannot parse log file format for fileName: {0}";
        private const string CannotFindFilenameInPathExceptionFormat =
            "Cannot find filename in path: {0}";

        internal static FileNameComponents ParseLogFileName(string logFilePath)
        {
            var name = Path.GetFileNameWithoutExtension(logFilePath);
            if(name == null)
            {
                throw new Exception(string.Format(CannotFindFilenameInPathExceptionFormat, logFilePath));
            }

            var sequence = 0u;
            var matches = LogFileFormat.Match(name);
            if(!matches.Success)
            {
                throw new Exception(string.Format(CannotParseLogFileForFilenameExceptionFormat, name));
            }

            var nameMatch = matches.Groups["name"];
            if (nameMatch.Success)
            {
                name = nameMatch.Value;
            }

            var digitMatch = matches.Groups["digits"];
            if (digitMatch.Success)
            {
                var sequenceMatch = digitMatch.Value;
                sequence = uint.Parse(sequenceMatch);
            }

            var extension = (Path.GetExtension(logFilePath) ?? string.Empty).TrimStart('.');
            return new FileNameComponents(name, sequence, extension);
        }
    }
}