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

using Serilog.Parsing;

namespace Serilog.Core
{
    /// <summary>
    /// An efficient store of parsed message templates.
    /// </summary>
    public interface IMessageTemplateCache
    {
        /// <summary>
        /// Get the parsed representation of the template. If the
        /// template has not been seen before, the template will be
        /// parsed, cached, and then returned.
        /// </summary>
        /// <param name="messageTemplate">The message template to get.</param>
        /// <returns>The parsed representation of the template.</returns>
        /// <seealso cref="MessageTemplateParser"/>
        MessageTemplate GetParsedTemplate(string messageTemplate);
    }
}