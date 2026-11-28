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

using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Transformations;
using Serilog.Templates.Compilation;
using Serilog.Templates.Themes;
using ConstantExpression = Serilog.Expressions.Ast.ConstantExpression;
using Expression = Serilog.Expressions.Ast.Expression;
using ParameterExpression = System.Linq.Expressions.ParameterExpression;
using LX = System.Linq.Expressions.Expression;
using ExpressionBody = System.Linq.Expressions.Expression;
// ReSharper disable UseIndexFromEndExpression

namespace Serilog.Expressions.Compilation.Linq;

class LinqExpressionCompiler : SerilogExpressionTransformer<ExpressionBody>
{
    readonly NameResolver _nameResolver;
    readonly IFormatProvider? _formatProvider;

    static readonly MethodInfo CollectSequenceElementsMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.CollectSequenceElements), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ExtendSequenceValueWithSpreadMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ExtendSequenceValueWithSpread), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ExtendSequenceValueWithItemMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ExtendSequenceValueWithItem), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ConstructSequenceValueMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ConstructSequenceValue), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo CollectStructurePropertiesMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.CollectStructureProperties), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ConstructStructureValueMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ConstructStructureValue), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo CompleteStructureValueMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.CompleteStructureValue), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ExtendStructureValueWithSpreadMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ExtendStructureValueWithSpread), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo ExtendStructureValueWithPropertyMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.ExtendStructureValueWithProperty), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo CoerceToScalarBooleanMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.CoerceToScalarBoolean), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo IndexOfMatchMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.IndexOfMatch), BindingFlags.Static | BindingFlags.Public)!;

    static readonly MethodInfo TryGetStructurePropertyValueMethod = typeof(Intrinsics)
        .GetMethod(nameof(Intrinsics.TryGetStructurePropertyValue), BindingFlags.Static | BindingFlags.Public)!;

    static readonly PropertyInfo EvaluationContextLogEventProperty = typeof(EvaluationContext)
        .GetProperty(nameof(EvaluationContext.LogEvent), BindingFlags.Instance | BindingFlags.Public)!;

    ParameterExpression Context { get; } = LX.Variable(typeof(EvaluationContext), "ctx");

    LinqExpressionCompiler(IFormatProvider? formatProvider, NameResolver nameResolver)
    {
        _nameResolver = nameResolver;
        _formatProvider = formatProvider;
    }

    public static Evaluatable Compile(Expression expression, IFormatProvider? formatProvider,
        NameResolver nameResolver)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));
        var compiler = new LinqExpressionCompiler(formatProvider, nameResolver);
        var body = compiler.Transform(expression);
        return LX.Lambda<Evaluatable>(body, compiler.Context).Compile();
    }

    ExpressionBody Splice(Expression<Evaluatable> lambda)
    {
        return ParameterReplacementVisitor.ReplaceParameters(lambda, Context);
    }

    protected override ExpressionBody Transform(CallExpression call)
    {
        if (!_nameResolver.TryResolveFunctionName(call.OperatorName, out var m))
            throw new ExpressionValidationException($"The function name `{call.OperatorName}` was not recognized.");

        var methodParameters = m.GetParameters()
            .Select(info => (pi: info, optional: info.GetCustomAttribute<OptionalAttribute>() != null))
            .ToList();

        // Log warning for CI modifier usage on functions that don't support it
        // Note: We log a warning rather than throwing to maintain backward compatibility
        // Previously, invalid CI usage was silently ignored
        if (call.IgnoreCase)
        {
            var supportsStringComparison = methodParameters.Any(p => p.pi.ParameterType == typeof(StringComparison));
            if (!supportsStringComparison)
            {
                SelfLog.WriteLine($"The function `{call.OperatorName}` does not support case-insensitive operation; the 'ci' modifier will be ignored.");
            }
        }

        var allowedParameters = methodParameters.Where(info => info.pi.ParameterType == typeof(LogEventPropertyValue)).ToList();
        var requiredParameterCount = allowedParameters.Count(info => !info.optional);

        if (call.Operands.Length < requiredParameterCount || call.Operands.Length > allowedParameters.Count)
        {
            var requirements = DescribeRequirements(allowedParameters.Select(info => (info.pi.Name!, info.optional)).ToList());
            throw new ArgumentException($"The function `{call.OperatorName}` {requirements}.");
        }

        var operands = new Queue<LX>(call.Operands.Select(Transform));

        // `and` and `or` short-circuit to save execution time; unlike the earlier Serilog.Filters.Expressions, nothing else does.
        if (Operators.SameOperator(call.OperatorName, Operators.RuntimeOpAnd))
            return CompileLogical(LX.AndAlso, operands.Dequeue(), operands.Dequeue());

        if (Operators.SameOperator(call.OperatorName, Operators.RuntimeOpOr))
            return CompileLogical(LX.OrElse, operands.Dequeue(), operands.Dequeue());

        var boundParameters = new List<LX>(methodParameters.Count);
        foreach (var (pi, optional) in methodParameters)
        {
            if (pi.ParameterType == typeof(LogEventPropertyValue))
            {
                boundParameters.Add(operands.Count > 0
                    ? operands.Dequeue()
                    : LX.Constant(null, typeof(LogEventPropertyValue)));
            }
            else if (pi.ParameterType == typeof(StringComparison))
                boundParameters.Add(LX.Constant(call.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
            else if (pi.ParameterType == typeof(IFormatProvider))
                boundParameters.Add(LX.Constant(_formatProvider, typeof(IFormatProvider)));
            else if (pi.ParameterType == typeof(LogEvent))
                boundParameters.Add(LX.Property(Context, EvaluationContextLogEventProperty));
            else if (_nameResolver.TryBindFunctionParameter(pi, out var binding))
                boundParameters.Add(LX.Constant(binding, pi.ParameterType));
            else if (optional)
                boundParameters.Add(LX.Constant(
                    pi.GetCustomAttribute<DefaultParameterValueAttribute>()?.Value, pi.ParameterType));
            else
                throw new ArgumentException($"The method `{m.Name}` implementing function `{call.OperatorName}` has argument `{pi.Name}` which could not be bound.");
        }

        return LX.Call(m, boundParameters);
    }

    static string DescribeRequirements(IReadOnlyList<(string name, bool optional)> parameters)
    {
        static string DescribeArgument((string name, bool optional) p) =>
            $"`{p.name}`" + (p.optional ? " (optional)" : "");

        if (parameters.Count == 0)
            return "accepts no arguments";

        if (parameters.Count == 1)
            return $"accepts one argument, {DescribeArgument(parameters[0])}";

        if (parameters.Count == 2)
            return $"accepts two arguments, {DescribeArgument(parameters[0])} and {DescribeArgument(parameters[1])}";

        var result = new StringBuilder("accepts arguments");
        for (var i = 0; i < parameters.Count - 1; ++i)
            result.Append($" {DescribeArgument(parameters[i])},");

        result.Append($" and {DescribeArgument(parameters[parameters.Count - 1])}");
        return result.ToString();
    }

    static ExpressionBody CompileLogical(Func<ExpressionBody, ExpressionBody, ExpressionBody> apply, ExpressionBody lhs, ExpressionBody rhs)
    {
        return LX.Convert(
            LX.New(
                typeof(ScalarValue).GetConstructor([typeof(object)])!,
                LX.Convert(apply(
                    LX.Call(CoerceToScalarBooleanMethod, lhs),
                    LX.Call(CoerceToScalarBooleanMethod, rhs)), typeof(object))),
            typeof(LogEventPropertyValue));
    }

    protected override ExpressionBody Transform(AccessorExpression spx)
    {
        var receiver = Transform(spx.Receiver);
        return LX.Call(TryGetStructurePropertyValueMethod, LX.Constant(StringComparison.Ordinal), receiver, LX.Constant(spx.MemberName, typeof(string)));
    }
    protected override ExpressionBody Transform(ConstantExpression cx)
    {
        return LX.Constant(cx.Constant);
    }

    protected override ExpressionBody Transform(AmbientNameExpression px)
    {
        if (px.IsBuiltIn)
        {
            var formatter = new CompiledMessageToken(_formatProvider, null, TemplateTheme.None);
            var formatProvider = _formatProvider;

            return px.PropertyName switch
            {
                BuiltInProperty.Level => Splice(context => new ScalarValue(context.LogEvent.Level)),
                BuiltInProperty.Message => Splice(context => new ScalarValue(Intrinsics.RenderMessage(formatter, context))),
                BuiltInProperty.Exception => Splice(context =>
                    context.LogEvent.Exception == null ? null : new ScalarValue(context.LogEvent.Exception)),
                BuiltInProperty.TraceId => Splice(context =>
                    context.LogEvent.TraceId == null ? null : new ScalarValue(context.LogEvent.TraceId.Value)),
                BuiltInProperty.SpanId => Splice(context =>
                    context.LogEvent.SpanId == null ? null : new ScalarValue(context.LogEvent.SpanId.Value)),
                BuiltInProperty.Timestamp => Splice(context => new ScalarValue(context.LogEvent.Timestamp)),
                BuiltInProperty.MessageTemplate => Splice(context => new ScalarValue(context.LogEvent.MessageTemplate.Text)),
                BuiltInProperty.Properties => Splice(context =>
                    new StructureValue(context.LogEvent.Properties.Select(kvp => new LogEventProperty(kvp.Key, kvp.Value)),
                        null)),
                BuiltInProperty.Renderings => Splice(context => Intrinsics.GetRenderings(context.LogEvent, formatProvider)),
                BuiltInProperty.EventId => Splice(context =>
                    new ScalarValue(EventIdHash.Compute(context.LogEvent.MessageTemplate.Text))),
                var alias when _nameResolver.TryResolveBuiltInPropertyName(alias, out var target) =>
                    Transform(new AmbientNameExpression(target, true)),
                _ => LX.Constant(null, typeof(LogEventPropertyValue))
            };
        }

        // Don't close over the AST node.
        var propertyName = px.PropertyName;
        return Splice(context => Intrinsics.GetPropertyValue(context, propertyName));
    }

    protected override ExpressionBody Transform(LocalNameExpression nlx)
    {
        // Don't close over the AST node.
        var name = nlx.Name;
        return Splice(context => Intrinsics.GetLocalValue(context, name));
    }

    protected override ExpressionBody Transform(Ast.LambdaExpression lmx)
    {
        var parameters = lmx.Parameters.Select(px => Tuple.Create(px, LX.Parameter(typeof(LogEventPropertyValue), px.ParameterName))).ToList();
        var paramSwitcher = new ExpressionConstantMapper(parameters.ToDictionary(px => (object)px.Item1, px => (System.Linq.Expressions.Expression)px.Item2));
        var rewritten = paramSwitcher.Visit(Transform(lmx.Body));

        Type delegateType;
        if (lmx.Parameters.Length == 1)
            delegateType = typeof(Func<LogEventPropertyValue, LogEventPropertyValue>);
        else if (lmx.Parameters.Length == 2)
            delegateType = typeof(Func<LogEventPropertyValue, LogEventPropertyValue, LogEventPropertyValue>);
        else
            throw new NotSupportedException("Unsupported lambda signature.");

        var lambda = LX.Lambda(delegateType, rewritten!, parameters.Select(px => px.Item2).ToArray());

        // Unfortunately, right now, functions need to be threaded through in constant scalar values :-D
        return LX.New(typeof(ScalarValue).GetConstructor([typeof(object)])!,
            LX.Convert(lambda, typeof(object)));
    }

    protected override ExpressionBody Transform(Ast.ParameterExpression prx)
    {
        // Will be within a lambda, which will subsequently sub-in the actual value.
        // The `prx` placeholder needs to be wrapped in a `ScalarValue` so that eager
        // typechecking doesn't fail before we've substituted the real value in.
        return LX.Constant(new ScalarValue(prx), typeof(LogEventPropertyValue));
    }

    protected override ExpressionBody Transform(IndexerWildcardExpression wx)
    {
        return LX.Constant(null, typeof(LogEventPropertyValue));
    }

    protected override ExpressionBody Transform(ArrayExpression ax)
    {
        var elements = new List<ExpressionBody>(ax.Elements.Length);
        var i = 0;
        for (; i < ax.Elements.Length; ++i)
        {
            var element = ax.Elements[i];
            if (element is ItemElement item)
                elements.Add(Transform(item.Value));
            else
                break;
        }

        var arr = LX.NewArrayInit(typeof(LogEventPropertyValue), elements.ToArray());
        var collected = LX.Call(CollectSequenceElementsMethod, arr);

        for (; i < ax.Elements.Length; ++i)
        {
            var element = ax.Elements[i];
            if (element is ItemElement item)
                collected = LX.Call(ExtendSequenceValueWithItemMethod, collected, Transform(item.Value));
            else
            {
                var spread = (SpreadElement) element;
                collected = LX.Call(ExtendSequenceValueWithSpreadMethod, collected, Transform(spread.Content));
            }
        }

        return LX.Call(ConstructSequenceValueMethod, collected);
    }

    protected override ExpressionBody Transform(ObjectExpression ox)
    {
        var names = new List<string>();
        var values = new List<ExpressionBody>();

        var i = 0;
        for (; i < ox.Members.Length; ++i)
        {
            var member = ox.Members[i];
            if (member is PropertyMember property)
            {
                if (names.Contains(property.Name))
                {
                    var oldPos = names.IndexOf(property.Name);
                    values[oldPos] = Transform(property.Value);
                }
                else
                {
                    names.Add(property.Name);
                    values.Add(Transform(property.Value));
                }
            }
            else
            {
                break;
            }
        }

        var namesConstant = LX.Constant(names.ToArray(), typeof(string[]));
        var valuesArr = LX.NewArrayInit(typeof(LogEventPropertyValue), values.ToArray());
        var properties = LX.Call(CollectStructurePropertiesMethod, namesConstant, valuesArr);

        if (i == ox.Members.Length)
        {
            // No spreads; more efficient than `Complete*` because erasure is not required.
            return LX.Call(ConstructStructureValueMethod, properties);
        }

        for (; i < ox.Members.Length; ++i)
        {
            var member = ox.Members[i];
            if (member is PropertyMember property)
            {
                properties = LX.Call(
                    ExtendStructureValueWithPropertyMethod,
                    properties,
                    LX.Constant(property.Name),
                    Transform(property.Value));
            }
            else
            {
                var spread = (SpreadMember) member;
                properties = LX.Call(
                    ExtendStructureValueWithSpreadMethod,
                    properties,
                    Transform(spread.Content));
            }
        }

        return LX.Call(CompleteStructureValueMethod, properties);
    }

    protected override ExpressionBody Transform(IndexerExpression ix)
    {
        return Transform(new CallExpression(false, Operators.OpElementAt, ix.Receiver, ix.Index));
    }

    protected override ExpressionBody Transform(IndexOfMatchExpression mx)
    {
        var rx = LX.Constant(mx.Regex);
        var target = Transform(mx.Corpus);
        return LX.Call(IndexOfMatchMethod, target, rx);
    }
}