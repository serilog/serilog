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
    // Rolls files based on the current date, using a path
    // formatting pattern like:
    //    Logs/log-{Date}.txt
    //
    class TemplatedPathRoller
    {
        const string OldStyleDateSpecifier = "{0}";
        const string DateSpecifier = "{Date}";
        const string DateFormat = "yyyyMMdd";
        const string DefaultSeparator = "-";

        readonly string _pathTemplate;
        readonly string _directorySearchPattern;
        readonly string _directory;
        readonly Regex _filenameMatcher;

        public TemplatedPathRoller(string pathTemplate)
        {
            if (pathTemplate == null) throw new ArgumentNullException("pathTemplate");
            if (pathTemplate.Contains(OldStyleDateSpecifier))
                throw new ArgumentException("The old-style date specifier " + OldStyleDateSpecifier +
                    " is no longer supported, instead please use " + DateSpecifier);

            var directory = Path.GetDirectoryName(pathTemplate);
            if (string.IsNullOrEmpty(directory))
            {
#if ASPNETCORE50
                directory = Directory.GetCurrentDirectory();
#else
                directory = Environment.CurrentDirectory;
#endif
            }

            directory = Path.GetFullPath(directory);

            if (directory.Contains(DateSpecifier))
                throw new ArgumentException("The date cannot form part of the directory name");

            var filenameTemplate = Path.GetFileName(pathTemplate);
            if (!filenameTemplate.Contains(DateSpecifier))
            {
                filenameTemplate = Path.GetFileNameWithoutExtension(filenameTemplate) + DefaultSeparator +
                    DateSpecifier + Path.GetExtension(filenameTemplate);
            }

            var indexOfSpecifier = filenameTemplate.IndexOf(DateSpecifier, StringComparison.Ordinal);
            var prefix = filenameTemplate.Substring(0, indexOfSpecifier);
            var suffix = filenameTemplate.Substring(indexOfSpecifier + DateSpecifier.Length);
            _filenameMatcher = new Regex(
                "^" +
                Regex.Escape(prefix) +
                "(?<date>\\d{" + DateFormat.Length + "})" + 
                "(?<inc>_[0-9]{3,}){0,1}" +
                Regex.Escape(suffix) +
                "$");

            _directorySearchPattern = filenameTemplate.Replace(DateSpecifier, "*");
            _directory = directory;
            _pathTemplate = Path.Combine(_directory, filenameTemplate); 
        }

        public string LogFileDirectory { get { return _directory; } }

        public string DirectorySearchPattern { get { return _directorySearchPattern; } }

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
                var match = _filenameMatcher.Match(filename);
                if (match.Success)
                {
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
}
