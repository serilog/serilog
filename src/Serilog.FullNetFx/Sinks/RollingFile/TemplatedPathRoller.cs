// Copyright 2013 Nicholas Blumhardt
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Serilog.Sinks.RollingFile
{
    // Rolls files based on the current date, using a path
    // formatting pattern like:
    //    Logs/log-{Date}.txt
    // In future, additional rolling strategies should be able
    // to be implemented using patterns like:
    //    Logs/log-{Hour}.txt
    //    Logs/log-{Minute}.txt
    //    Logs/log-{Now:yyyyMMdd-HH:mm.SS}
    // I.e. tokens in the log format, while not strictly equivalent
    // to the message template DSL, permit different rolling
    // strategies to be communicated without broadening
    // the API or breaking consumers.
    class TemplatedPathRoller
    {
        const string OldStyleDateSpecifier = "{0}";
        const string DateSpecifier = "{Date}";
        const string DateFormat = "yyyyMMdd";
        const string DefaultExtension = ".txt";
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

            var directory = Path.GetDirectoryName(pathTemplate) ?? "";
            if (directory.Contains(DateSpecifier))
                throw new ArgumentException("The date cannot form part of the directory name");

            var filenameTemplate = Path.GetFileName(pathTemplate) ?? DateSpecifier + DefaultExtension;
            if (!filenameTemplate.Contains(DateSpecifier))
            {
                filenameTemplate = Path.GetFileNameWithoutExtension(filenameTemplate) + DefaultSeparator +
                    DateSpecifier + (Path.GetExtension(filenameTemplate) ?? "");
            }

            var indexOfSpecifier = filenameTemplate.IndexOf(DateSpecifier, StringComparison.Ordinal);
            var prefix = filenameTemplate.Substring(0, indexOfSpecifier);
            var suffix = filenameTemplate.Substring(indexOfSpecifier + DateSpecifier.Length);
            _filenameMatcher = new Regex(
                "^" +
                Regex.Escape(prefix) +
                "\\d{" + DateFormat.Length + "}" + 
                Regex.Escape(suffix) +
                "$");

            _directorySearchPattern = filenameTemplate.Replace(DateSpecifier, "*");
            _directory = directory;
            _pathTemplate = Path.Combine(_directory, filenameTemplate);            
        }

        public string LogFileDirectory { get { return _directory; } }

        public string DirectorySearchPattern { get { return _directorySearchPattern; } }

        public void GetLogFilePath(DateTime now, out string path, out DateTime nextCheckpoint)
        {
            path = _pathTemplate.Replace(DateSpecifier, now.Date.ToString(DateFormat));
            nextCheckpoint = now.Date.AddDays(1);
        }

        public IEnumerable<string> OrderMatchingByAge(IEnumerable<string> fileNames)
        {
            return fileNames
                .Where(fn => _filenameMatcher.IsMatch(fn))
                .OrderByDescending(fn => fn);
        }
    }
}
