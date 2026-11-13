using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Serilog.Events;

namespace Serilog.Expressions.Tests.Support;

public class TestHelperNameResolver: NameResolver
{
    public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        if (name == "test_dict")
        {
            implementation = GetType().GetMethod(nameof(TestDict))!;
            return true;
        }

        implementation = null;
        return false;
    }

    public static LogEventPropertyValue? TestDict(LogEventPropertyValue? value)
    {
        if (value is not StructureValue sv)
            return null;

        return new DictionaryValue(sv.Properties.Select(kv =>
            KeyValuePair.Create(new ScalarValue(kv.Name), kv.Value)));
    }
}
