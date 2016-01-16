// Copyright 2013-2015 Serilog Contributors
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

namespace Serilog.Parsing
{
    /// <summary>
    /// A structure representing the alignment settings to apply when rendering a property.
    /// </summary>
    public struct Alignment
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Alignment"/>.
        /// </summary>
        /// <param name="direction">The text alignment direction.</param>
        /// <param name="width">The width of the text, in characters.</param>
        public Alignment(AlignmentDirection direction, int width)
        {
            Direction = direction;
            Width = width;
        }

        /// <summary>
        /// The text alignment direction.
        /// </summary>
        public AlignmentDirection Direction { get; }

        /// <summary>
        /// The width of the text.
        /// </summary>
        public int Width { get; }
    }
}
