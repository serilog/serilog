// Copyright 2016 Serilog Contributors
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

namespace Serilog.Data;

/// <summary>
/// A base class for visitors that rewrite the value with modifications. For example, implementations
/// might remove all structure properties with a certain name, apply size/length limits, or convert scalar properties of
/// one type into scalar properties of another.
/// </summary>
/// <typeparam name="TState"></typeparam>
public abstract class LogEventPropertyValueRewriter<TState> : LogEventPropertyValueVisitor<TState, LogEventPropertyValue>
{
    /// <summary>
    /// Visit a <see cref="ScalarValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="scalar">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="scalar"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="scalar"/> is <code>null</code></exception>
    protected override LogEventPropertyValue VisitScalarValue(TState state, ScalarValue scalar)
    {
        return Guard.AgainstNull(scalar);
    }

    /// <summary>
    /// Visit a <see cref="SequenceValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="sequence">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="sequence"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="sequence"/> is <code>null</code></exception>
    protected override LogEventPropertyValue VisitSequenceValue(TState state, SequenceValue sequence)
    {
        Guard.AgainstNull(sequence);

        for (var i = 0; i < sequence.Elements.Count; ++i)
        {
            var original = sequence.Elements[i];
            if (!ReferenceEquals(original, Visit(state, original)))
            {
                var contents = new LogEventPropertyValue[sequence.Elements.Count];

                // There's no need to visit any earlier elements: they all evaluated to
                // a reference equal with the original so just fill in the array up until `i`.
                for (var j = 0; j < i; ++j)
                {
                    contents[j] = sequence.Elements[j];
                }

                for (var k = i; k < contents.Length; ++k)
                {
                    contents[k] = Visit(state, sequence.Elements[k]);
                }

                return new SequenceValue(contents);
            }
        }

        return sequence;
    }

    /// <summary>
    /// Visit a <see cref="StructureValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="structure">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="structure"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="structure"/> is <code>null</code></exception>
    protected override LogEventPropertyValue VisitStructureValue(TState state, StructureValue structure)
    {
        Guard.AgainstNull(structure);

        for (var i = 0; i < structure.Properties.Count; ++i)
        {
            var original = structure.Properties[i];
            if (!ReferenceEquals(original.Value, Visit(state, original.Value)))
            {
                var properties = new LogEventProperty[structure.Properties.Count];

                // There's no need to visit any earlier elements: they all evaluated to
                // a reference equal with the original so just fill in the array up until `i`.
                for (var j = 0; j < i; ++j)
                {
                    properties[j] = structure.Properties[j];
                }

                for (var k = i; k < properties.Length; ++k)
                {
                    var property = structure.Properties[k];
                    properties[k] = new LogEventProperty(property.Name, Visit(state, property.Value));
                }

                return new StructureValue(properties, structure.TypeTag);
            }
        }

        return structure;
    }

    /// <summary>
    /// Visit a <see cref="DictionaryValue"/> value.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="dictionary">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="dictionary"/> is <code>null</code></exception>
    protected override LogEventPropertyValue VisitDictionaryValue(TState state, DictionaryValue dictionary)
    {
        Guard.AgainstNull(dictionary);

        foreach (var original in dictionary.Elements)
        {
            if (!ReferenceEquals(original.Value, Visit(state, original.Value)))
            {
                var elements = new Dictionary<ScalarValue, LogEventPropertyValue>(dictionary.Elements.Count);
                foreach (var element in dictionary.Elements)
                {
                    elements[element.Key] = Visit(state, element.Value);
                }

                return new DictionaryValue(elements);
            }
        }

        return dictionary;
    }

    /// <summary>
    /// Visit a value of an unsupported type. Returns the value unchanged.
    /// </summary>
    /// <param name="state">Operation state.</param>
    /// <param name="value">The value to visit.</param>
    /// <returns>The result of visiting <paramref name="value"/>.</returns>
    // ReSharper disable once UnusedParameter.Global
    // ReSharper disable once VirtualMemberNeverOverriden.Global
    protected override LogEventPropertyValue VisitUnsupportedValue(TState state, LogEventPropertyValue value)
    {
        return value;
    }
}
