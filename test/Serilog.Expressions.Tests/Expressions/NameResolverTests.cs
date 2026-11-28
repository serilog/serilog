using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Serilog.Events;
using Serilog.Expressions.Runtime;
using Serilog.Expressions.Tests.Support;
using Xunit;

namespace Serilog.Expressions.Tests.Expressions;

public class NameResolverTests
{
    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? Magic(LogEventPropertyValue? number)
    {
        if (!Coerce.Numeric(number, out var num))
            return null;

        return new ScalarValue(num + 42);
    }

    // ReSharper disable once UnusedMember.Global
    public static LogEventPropertyValue? SecretWordAt(string word, LogEventPropertyValue? index)
    {
        if (!Coerce.Numeric(index, out var i))
            return null;

        return new ScalarValue(word[(int)i].ToString());
    }

    class SecretWordResolver : NameResolver
    {
        readonly NameResolver _inner;
        readonly string _word;

        public SecretWordResolver(NameResolver inner, string word)
        {
            _inner = inner;
            _word = word;
        }

        public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
            => _inner.TryResolveFunctionName(name, out implementation);

        public override bool TryBindFunctionParameter(ParameterInfo parameter, [MaybeNullWhen(false)] out object boundValue)
        {
            if (parameter.ParameterType == typeof(string))
            {
                boundValue = _word;
                return true;
            }

            boundValue = null;
            return false;
        }
    }

    class LegacyLevelPropertyNameResolver: NameResolver
    {
        public override bool TryResolveBuiltInPropertyName(string alias, [NotNullWhen(true)] out string? target)
        {
            if (alias == "Level")
            {
                target = "l";
                return true;
            }

            target = null;
            return false;
        }
    }

    [Fact]
    public void UserDefinedFunctionsAreCallableInExpressions()
    {
        var expr = SerilogExpression.Compile(
            "magic(10) + 3 = 55",
            nameResolver: new StaticMemberNameResolver(typeof(NameResolverTests)));
        Assert.True(Coerce.IsTrue(expr(Some.InformationEvent())));
    }

    [Fact]
    public void UserDefinedFunctionsCanReceiveUserProvidedParameters()
    {
        var expr = SerilogExpression.Compile(
            "SecretWordAt(1) = 'e'",
            nameResolver: new SecretWordResolver(new StaticMemberNameResolver(typeof(NameResolverTests)), "hello"));
        Assert.True(Coerce.IsTrue(expr(Some.InformationEvent())));
    }

    [Fact]
    public void BuiltInPropertiesCanBeAliased()
    {
        var expr = SerilogExpression.Compile(
            "@Level = 'Information'",
            nameResolver: new LegacyLevelPropertyNameResolver());
        Assert.True(Coerce.IsTrue(expr(Some.InformationEvent())));
    }
}