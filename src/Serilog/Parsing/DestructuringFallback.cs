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

namespace Serilog.Parsing;

/// <summary>
/// A structure representing the fallback settings to apply as fallback on default destructuring.
/// </summary>
public readonly struct DestructuringFallback
{
    /// <summary>
    /// Initializes a new instance of <see cref="DestructuringFallback"/>.
    /// </summary>
    /// <param name="destructuring">The fallback destructuring.</param>
    /// <param name="applyToInheritance">If true, also inherited types fill use the fallback. Otherwise, only the specified type.</param>
    public DestructuringFallback(Destructuring destructuring, bool applyToInheritance = false)
    {
        Destructuring = destructuring;
        ApplyToInheritance = applyToInheritance;
    }

    /// <summary>
    /// The fallback destructuring.
    /// </summary>
    public Destructuring Destructuring { get; }

    /// <summary>
    /// If true, also inherited types fill use the fallback. Otherwise, only the specified type.
    /// </summary>
    public bool ApplyToInheritance { get; }
}
