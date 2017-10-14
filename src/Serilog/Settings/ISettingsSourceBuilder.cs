// Copyright 2013-2017 Serilog Contributors
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

namespace Serilog.Settings
{
    /// <summary>
    /// A builder that allows to combine sources of key-value settings in a fluent way.
    /// </summary>
    public interface ISettingsSourceBuilder
    {
        /// <summary>
        /// Adds a <see cref="ISettingsSource"/> after the already defined sources to the combined source
        /// </summary>
        /// <param name="settingSource">a source of key-value settings</param>
        /// <returns>a <see cref="ISettingsSourceBuilder"/> with the added source to allow chaining</returns>
        ISettingsSourceBuilder AddSource(ISettingsSource settingSource);
    }
}
