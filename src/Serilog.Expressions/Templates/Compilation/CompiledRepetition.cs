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

using Serilog.Events;
using Serilog.Expressions;
using Serilog.Expressions.Runtime;

namespace Serilog.Templates.Compilation;

class CompiledRepetition : CompiledTemplate
{
    readonly Evaluatable _enumerable;
    readonly string? _keyOrElementName;
    readonly string? _valueOrIndexName;
    readonly CompiledTemplate _body;
    readonly CompiledTemplate? _delimiter;
    readonly CompiledTemplate? _alternative;

    public CompiledRepetition(
        Evaluatable enumerable,
        string? keyOrElementName,
        string? valueOrIndexName,
        CompiledTemplate body,
        CompiledTemplate? delimiter,
        CompiledTemplate? alternative)
    {
        _enumerable = enumerable;
        _keyOrElementName = keyOrElementName;
        _valueOrIndexName = valueOrIndexName;
        _body = body;
        _delimiter = delimiter;
        _alternative = alternative;
    }

    public override void Evaluate(EvaluationContext ctx, TextWriter output)
    {
        var enumerable = _enumerable(ctx);
        if (enumerable is null or ScalarValue)
        {
            _alternative?.Evaluate(ctx, output);
            return;
        }

        if (enumerable is SequenceValue sequence)
        {
            if (sequence.Elements.Count == 0)
            {
                _alternative?.Evaluate(ctx, output);
                return;
            }

            for (var i = 0; i < sequence.Elements.Count; ++i)
            {
                // Null elements should have been invalid but Serilog didn't check, and so this does occur in the wild.
                var element = sequence.Elements[i] ?? new ScalarValue(null);

                if (i != 0)
                {
                    _delimiter?.Evaluate(ctx, output);
                }

                var local = _keyOrElementName != null
                    ? new EvaluationContext(ctx.LogEvent, Locals.Set(ctx.Locals, _keyOrElementName, element))
                    : ctx;

                local = _valueOrIndexName != null
                    ? new EvaluationContext(local.LogEvent, Locals.Set(local.Locals, _valueOrIndexName, new ScalarValue(i)))
                    : local;

                _body.Evaluate(local, output);
            }

            return;
        }

        if (enumerable is StructureValue structure)
        {
            if (structure.Properties.Count == 0)
            {
                _alternative?.Evaluate(ctx, output);
                return;
            }

            var first = true;
            foreach (var member in structure.Properties)
            {
                if (first)
                    first = false;
                else
                    _delimiter?.Evaluate(ctx, output);

                var local = _keyOrElementName != null
                    ? new(ctx.LogEvent, Locals.Set(ctx.Locals, _keyOrElementName, new ScalarValue(member.Name)))
                    : ctx;

                local = _valueOrIndexName != null
                    ? new(local.LogEvent, Locals.Set(local.Locals, _valueOrIndexName, member.Value))
                    : local;

                _body.Evaluate(local, output);
            }
        }

        if (enumerable is DictionaryValue dict)
        {
            if (dict.Elements.Count == 0)
            {
                _alternative?.Evaluate(ctx, output);
                return;
            }

            var first = true;
            foreach (var element in dict.Elements)
            {
                if (first)
                    first = false;
                else
                    _delimiter?.Evaluate(ctx, output);

                var local = _keyOrElementName != null
                    ? new(ctx.LogEvent, Locals.Set(ctx.Locals, _keyOrElementName, element.Key))
                    : ctx;

                local = _valueOrIndexName != null
                    ? new(local.LogEvent, Locals.Set(local.Locals, _valueOrIndexName, element.Value))
                    : local;

                _body.Evaluate(local, output);
            }
        }

        // Unsupported; not much we can do.
    }
}