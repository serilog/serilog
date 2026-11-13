using Serilog.Events;
using Serilog.Expressions;
using Serilog.Expressions.Runtime;
using Serilog.Parsing;
using Serilog.Templates.Compilation;
using Serilog.Templates.Themes;

namespace Serilog.Templates.Encoding
{
    class EscapableEncodedCompiledFormattedExpression : CompiledTemplate
    {
        static int _nextSubstituteLocalNameSuffix;
        readonly string _substituteLocalName = $"%sub{Interlocked.Increment(ref _nextSubstituteLocalNameSuffix)}";
        readonly Evaluatable _expression;
        readonly TemplateOutputEncoder _encoder;
        readonly CompiledFormattedExpression _inner;

        public EscapableEncodedCompiledFormattedExpression(Evaluatable expression, string? format, Alignment? alignment, IFormatProvider? formatProvider, TemplateTheme theme, TemplateOutputEncoder encoder)
        {
            _expression = expression;
            _encoder = encoder;
            
            // `expression` can't be passed through, because it may include calls to the `unsafe()` function (nested in arbitrary subexpressions) that
            // need to be evaluated first. So, instead, we evaluate `expression` and unwrap the result of `unsafe`, placing the result in a local variable
            // that the formatting expression we construct here can read from.
            _inner = new CompiledFormattedExpression(GetSubstituteLocalValue, format, alignment, formatProvider, theme);
        }

        LogEventPropertyValue? GetSubstituteLocalValue(EvaluationContext context)
        {
            return Locals.TryGetValue(context.Locals, _substituteLocalName, out var computed)
                ? computed
                : null;
        }

        public override void Evaluate(EvaluationContext ctx, TextWriter output)
        {
            var value = _expression(ctx);
            
            if (value is ScalarValue { Value: PreEncodedValue pv })
            {
                var rawContext = pv.Inner == null ?
                    new EvaluationContext(ctx.LogEvent) :
                    new EvaluationContext(ctx.LogEvent, Locals.Set(ctx.Locals, _substituteLocalName, pv.Inner));
                _inner.Evaluate(rawContext, output);
                return;
            }

            var buffer = new StringWriter(output.FormatProvider);
            
            var bufferedContext = value == null
                ? ctx
                : new EvaluationContext(ctx.LogEvent, Locals.Set(ctx.Locals, _substituteLocalName, value));

            _inner.Evaluate(bufferedContext, buffer);
            var encoded = _encoder.Encode(buffer.ToString());
            output.Write(encoded);
        }
    }
}