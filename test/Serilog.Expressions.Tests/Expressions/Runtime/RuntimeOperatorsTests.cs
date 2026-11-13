using System.Reflection;
using Serilog.Events;
using Serilog.Expressions.Runtime;
using Serilog.Expressions.Tests.Support;
using Xunit;

namespace Serilog.Expressions.Tests.Expressions.Runtime;

public class RuntimeOperatorsTests
{
    [Fact]
    public void InspectReadsPublicPropertiesFromScalarValue()
    {
        var message = Some.String();
        var ex = new DivideByZeroException(message);
        var scalar = new ScalarValue(ex);
        var inspected = RuntimeOperators.Inspect(scalar);
        var structure = Assert.IsType<StructureValue>(inspected);
        var asProperties = structure.Properties.ToDictionary(p => p.Name, p => p.Value);
        Assert.Contains("Message", asProperties);
        Assert.Contains("StackTrace", asProperties);
        var messageResult = Assert.IsType<ScalarValue>(asProperties["Message"]);
        Assert.Equal(message, messageResult.Value);
    }

    [Fact]
    public void DeepInspectionReadsSubproperties()
    {
        var innerMessage = Some.String();
        var inner = new DivideByZeroException(innerMessage);
        var ex = new TargetInvocationException(inner);
        var scalar = new ScalarValue(ex);
        var inspected = RuntimeOperators.Inspect(scalar, deep: new ScalarValue(true));
        var structure = Assert.IsType<StructureValue>(inspected);
        var innerStructure = Assert.IsType<StructureValue>(structure.Properties.Single(p => p.Name == "InnerException").Value);
        var innerMessageValue = Assert.IsType<ScalarValue>(innerStructure.Properties.Single(p => p.Name == "Message").Value);
        Assert.Equal(innerMessage, innerMessageValue.Value);
    }
}