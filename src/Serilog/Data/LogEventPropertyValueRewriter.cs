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

using System;
using System.Collections.Generic;
using Serilog.Events;

namespace Serilog.Data
{
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
        protected override LogEventPropertyValue VisitScalarValue(TState state, ScalarValue scalar)
        {
            if (scalar == null) throw new ArgumentNullException(nameof(scalar));
            return scalar;
        }

        /// <summary>
        /// Visit a <see cref="SequenceValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="sequence">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="sequence"/>.</returns>
        protected override LogEventPropertyValue VisitSequenceValue(TState state, SequenceValue sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));

            var contents = new LogEventPropertyValue[sequence.Elements.Count];
            for (var i = 0; i < contents.Length; ++i)
            {
                contents[i] = Visit(state, sequence.Elements[i]);
            }

            return new SequenceValue(contents);
        }

        /// <summary>
        /// Visit a <see cref="StructureValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="structure">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="structure"/>.</returns>
        protected override LogEventPropertyValue VisitStructureValue(TState state, StructureValue structure)
        {
            if (structure == null) throw new ArgumentNullException(nameof(structure));

            var properties = new LogEventProperty[structure.Properties.Count];
            for (var i = 0; i < properties.Length; ++i)
            {
                var property = structure.Properties[i];
                properties[i] = new LogEventProperty(property.Name, Visit(state, property.Value));
            }

            return new StructureValue(properties, structure.TypeTag);
        }

        /// <summary>
        /// Visit a <see cref="DictionaryValue"/> value.
        /// </summary>
        /// <param name="state">Operation state.</param>
        /// <param name="dictionary">The value to visit.</param>
        /// <returns>The result of visiting <paramref name="dictionary"/>.</returns>
        protected override LogEventPropertyValue VisitDictionaryValue(TState state, DictionaryValue dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            var elements = new Dictionary<ScalarValue, LogEventPropertyValue>(dictionary.Elements.Count);
            foreach (var element in dictionary.Elements)
            {
                elements[element.Key] = Visit(state, element.Value);
            }

            return new DictionaryValue(elements);
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
}
