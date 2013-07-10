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
using System.IO;

namespace Serilog.Sinks.RollingFile
{
    // Rolls files based on the current date;
    // In future, additional rolling strategies should be able
    // to be implemented using patterns like:
    //    Logs/log-{Hour}.txt
    //    Logs/log-{Minute}.txt
    //    Logs/log-{Now:yyyyMMdd-HH:mm.sss}
    // I.e. tokens in the log format, while not strictly equivalent
    // to the message template DSL, permit different rolling
    // strategies to be communicated without broadening
    // the API or breaking consumers.
    class TemplatedPathRoller
    {
        const string OldStyleDateSpecifier = "{0}";
        const string NewStyleDateSpecifier = "{Date}";
        const string DefaultExtension = ".txt";
        const string DefaultSeparator = "-";

        readonly string _pathTemplate;
        readonly string _filenameTemplate;
        readonly string _directory;

        public TemplatedPathRoller(string pathTemplate)
        {
            if (pathTemplate == null) throw new ArgumentNullException("pathTemplate");
            if (pathTemplate.Contains(OldStyleDateSpecifier))
                throw new ArgumentException("The old-style date specifier " + OldStyleDateSpecifier +
                    " is no longer supported, instead please use " + NewStyleDateSpecifier);

            var directory = Path.GetDirectoryName(pathTemplate) ?? "";
            if (directory.Contains(NewStyleDateSpecifier))
                throw new ArgumentException("The date cannot form part of the directory name");

            var filenameTemplate = Path.GetFileName(pathTemplate) ?? NewStyleDateSpecifier + DefaultExtension;
            if (!filenameTemplate.Contains(NewStyleDateSpecifier))
            {
                filenameTemplate = Path.GetFileNameWithoutExtension(filenameTemplate) + DefaultSeparator +
                    NewStyleDateSpecifier + (Path.GetExtension(filenameTemplate) ?? "");
            }

            _filenameTemplate = filenameTemplate;
            _directory = directory;
            _pathTemplate = Path.Combine(_directory, _filenameTemplate);
        }

        public void GetLogFilePath(DateTime now, out string path, out DateTime nextCheckpoint)
        {
            path = _pathTemplate.Replace(NewStyleDateSpecifier, now.Date.ToString("yyyyMMdd"));
            nextCheckpoint = now.Date.AddDays(1);
        }
    }
}
