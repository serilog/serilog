// Copyright 2014 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Serilog.Sinks.RollingFile
{
    ///<summary>
    /// Rolls files based on the current date using a path formatting pattern
    /// like: <c>Logs/log-{Date}.txt</c>
    /// </summary>
    /// <remarks>
    /// If the {Date} specifier is not found in the path template, it will be
    /// added prior to the file name extension. E.g. <c>Log.txt</c> becomes
    /// <c>Log-{Date}.txt</c>. Dates are automatically formatted as yyyyMMdd.
    /// E.g. this will create filenames like <c>Log-20151225.txt</c>.
    /// Sequences of files can also be generated and will be in the format:
    /// <c>Log-20151225_001.txt</c>.
    /// </remarks>
    class TemplatedPathRoller
    {
        const string OldStyleDateSpecifier = "{0}";
        const string DateSpecifier = "{Date}";
        const string DateFormat = "yyyyMMdd";
        const string DefaultSeparator = "-";

        readonly string _pathTemplate;
        readonly string _directorySearchPattern;
        readonly string _directory;
        readonly Regex _fileNameMatcher;

        public TemplatedPathRoller(string pathTemplate)
        {
            if (pathTemplate == null) throw new ArgumentNullException("pathTemplate");
            if (pathTemplate.Contains(OldStyleDateSpecifier))
                throw new ArgumentException(
                    string.Format(
                        "The old-style date specifier {0} is no longer supported, instead please use {1}",
                        OldStyleDateSpecifier,
                        DateSpecifier));

            var directory = GetFullPathOfDirectory(pathTemplate);
            if (directory.Contains(DateSpecifier))
                throw new ArgumentException("The date cannot form part of the directory name");

            var fileNameTemplate = GetFileNameTemplate(pathTemplate);

            _fileNameMatcher = CreateFileNameMatcher(fileNameTemplate);
            _directorySearchPattern = fileNameTemplate.Replace(DateSpecifier, "*");
            _directory = directory;
            _pathTemplate = Path.Combine(_directory, fileNameTemplate);
        }

        /// <summary>
        /// Gets the full path of the provided path, or the current directory
        /// if no explicit directory is provided
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static string GetFullPathOfDirectory(string path)
        {
            var exandedPath = Environment.ExpandEnvironmentVariables(path);
            var directory = Path.GetDirectoryName(exandedPath);
            if (string.IsNullOrEmpty(directory))
            {
#if ASPNETCORE50
                directory = Directory.GetCurrentDirectory();
#else
                directory = Environment.CurrentDirectory;
#endif
            }

            return Path.GetFullPath(directory);
        }

        /// <summary>
        /// Adds {Date} to the file name if not specified
        /// </summary>
        /// <param name="pathTemplate"></param>
        /// <returns></returns>
        static string GetFileNameTemplate(string pathTemplate)
        {
            var fileNameTemplate = Path.GetFileName(pathTemplate);
            if (fileNameTemplate != null && !fileNameTemplate.Contains(DateSpecifier))
            {
                fileNameTemplate = Path.GetFileNameWithoutExtension(fileNameTemplate) + DefaultSeparator +
                                   DateSpecifier + Path.GetExtension(fileNameTemplate);
            }
            return fileNameTemplate;
        }

        /// <summary>
        /// Creates a regex that matches files generated from this filename template.
        /// E.g. <c>Log-{Date}.txt</c> will match <c>Log-20151225.txt</c> but not <c>Log-123.txt</c>.
        /// </summary>
        /// <param name="filenameTemplate"></param>
        /// <returns></returns>
        static Regex CreateFileNameMatcher(string filenameTemplate)
        {
            var indexOfSpecifier = filenameTemplate.IndexOf(DateSpecifier, StringComparison.Ordinal);
            var prefix = filenameTemplate.Substring(0, indexOfSpecifier);
            var suffix = filenameTemplate.Substring(indexOfSpecifier + DateSpecifier.Length);
            return new Regex(
                "^" +
                Regex.Escape(prefix) +
                "(?<date>\\d{" + DateFormat.Length + "})" +
                "(?<inc>_[0-9]{3,}){0,1}" +
                Regex.Escape(suffix) +
                "$");
        }

        public string LogFileDirectory
        {
            get { return _directory; }
        }

        public string DirectorySearchPattern
        {
            get { return _directorySearchPattern; }
        }

        public void GetLogFilePath(DateTime date, int sequenceNumber, out string path)
        {
            var tok = date.ToString(DateFormat, CultureInfo.InvariantCulture);

            if (sequenceNumber != 0)
                tok += "_" + sequenceNumber.ToString("000", CultureInfo.InvariantCulture);

            path = _pathTemplate.Replace(DateSpecifier, tok);
        }

        public IEnumerable<RollingLogFile> SelectMatches(IEnumerable<string> filenames)
        {
            foreach (var filename in filenames)
            {
                var match = _fileNameMatcher.Match(filename);
                if (!match.Success) continue;
                var inc = 0;
                var incGroup = match.Groups["inc"];
                if (incGroup.Captures.Count != 0)
                {
                    var incPart = incGroup.Captures[0].Value.Substring(1);
                    inc = int.Parse(incPart, CultureInfo.InvariantCulture);
                }

                DateTime date;
                var datePart = match.Groups["date"].Captures[0].Value;
                if (!DateTime.TryParseExact(
                    datePart,
                    DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out date))
                    continue;

                yield return new RollingLogFile(filename, date, inc);
            }
        }
    }
}