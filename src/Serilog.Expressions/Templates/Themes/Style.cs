// Copyright © Serilog Contributors
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

namespace Serilog.Templates.Themes;

readonly struct Style
{
    readonly string? _ansiStyle;

    public Style(string ansiStyle)
    {
        _ansiStyle = ansiStyle;
    }

    internal StyleReset Set(TextWriter output, ref int invisibleCharacterCount)
    {
        if (_ansiStyle != null)
        {
            output.Write(_ansiStyle);
            invisibleCharacterCount += _ansiStyle.Length;
            invisibleCharacterCount += StyleReset.ResetCharCount;

            return new(output);
        }

        return default;
    }

    public string? GetAnsiStyle()
    {
        return _ansiStyle;
    }
}