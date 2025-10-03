using Serilog.Policies;

namespace Serilog.Tests.Policies;

public class SelectiveDestructuringPolicyTests
{
  class ComplexObject
  {
    public string? Name { get; set; }
    public int Id { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? InternalData { get; set; }
  }

  class NonMatchingType
  {
    public string? Value { get; set; }
  }

  [Fact]
  public void SelectiveDestructuringPolicyOnlyDestructuresSpecifiedType()
  {
    var policy = new SelectiveDestructuringPolicy<ComplexObject>("Name", "Id");

    var complexObject = new ComplexObject { Name = "Test", Id = 1, Status = "Active" };
    var nonMatchingObject = new NonMatchingType { Value = "test" };

    // Test that our policy returns true for ComplexObject
    var factory = new PropertyValueConverter(10, 1000, 1000, [], [], [], false);
    LogEventPropertyValue? result;
    var handled = policy.TryDestructure(complexObject, factory, out result);
    Assert.True(handled);
    Assert.NotNull(result);

    // Test that our policy returns false for NonMatchingType
    handled = policy.TryDestructure(nonMatchingObject, factory, out result);
    Assert.False(handled);
    Assert.Null(result);
  }

  [Fact]
  public void SelectiveDestructuringPolicyOnlyIncludesSelectedProperties()
  {
    var policy = new SelectiveDestructuringPolicy<ComplexObject>("Name", "Id", "Status");
    var factory = new PropertyValueConverter(10, 1000, 1000, [], [], [policy], false);

    var testObject = new ComplexObject
    {
      Name = "TestObject",
      Id = 123,
      Status = "Active",
      Description = "This should not be included",
      CreatedAt = new DateTime(2024, 1, 1),
      InternalData = "Secret data"
    };

    var result = factory.CreatePropertyValue(testObject, destructureObjects: true);

    Assert.IsType<StructureValue>(result);
    var structure = (StructureValue)result;

    // Should only contain the selected properties
    Assert.Equal(3, structure.Properties.Count);

    var propertyNames = structure.Properties.Select(p => p.Name).ToHashSet();
    Assert.Contains("Name", propertyNames);
    Assert.Contains("Id", propertyNames);
    Assert.Contains("Status", propertyNames);
    Assert.DoesNotContain("Description", propertyNames);
    Assert.DoesNotContain("CreatedAt", propertyNames);
    Assert.DoesNotContain("InternalData", propertyNames);

    // Verify property values
    var nameProperty = structure.Properties.First(p => p.Name == "Name");
    Assert.Equal("TestObject", ((ScalarValue)nameProperty.Value).Value);

    var idProperty = structure.Properties.First(p => p.Name == "Id");
    Assert.Equal(123, ((ScalarValue)idProperty.Value).Value);

    var statusProperty = structure.Properties.First(p => p.Name == "Status");
    Assert.Equal("Active", ((ScalarValue)statusProperty.Value).Value);
  }

  [Fact]
  public void SelectiveDestructuringPolicyHandlesNullPropertyValues()
  {
    var policy = new SelectiveDestructuringPolicy<ComplexObject>("Name", "Status");
    var factory = new PropertyValueConverter(10, 1000, 1000, [], [], [policy], false);

    var testObject = new ComplexObject
    {
      Name = null,
      Id = 456,
      Status = "Inactive"
    };

    var result = factory.CreatePropertyValue(testObject, destructureObjects: true);

    Assert.IsType<StructureValue>(result);
    var structure = (StructureValue)result;

    Assert.Equal(2, structure.Properties.Count);

    var nameProperty = structure.Properties.First(p => p.Name == "Name");
    Assert.IsType<ScalarValue>(nameProperty.Value);
    Assert.Null(((ScalarValue)nameProperty.Value).Value);

    var statusProperty = structure.Properties.First(p => p.Name == "Status");
    Assert.Equal("Inactive", ((ScalarValue)statusProperty.Value).Value);
  }

  [Fact]
  public void SelectiveDestructuringPolicyIgnoresNonExistentProperties()
  {
    var policy = new SelectiveDestructuringPolicy<ComplexObject>("Name", "NonExistentProperty");
    var factory = new PropertyValueConverter(10, 1000, 1000, [], [], [policy], false);

    var testObject = new ComplexObject
    {
      Name = "Test",
      Id = 789
    };

    var result = factory.CreatePropertyValue(testObject, destructureObjects: true);

    Assert.IsType<StructureValue>(result);
    var structure = (StructureValue)result;

    // Should only contain the existing property
    Assert.Single(structure.Properties);
    Assert.Equal("Name", structure.Properties.First().Name);
  }

  [Fact]
  public void SelectiveDestructuringPolicyWorksWithLoggerConfiguration()
  {
    var sink = new CollectingSink();
    var logger = new LoggerConfiguration()
      .Destructure.BySelecting<ComplexObject>("Name", "Id")
      .WriteTo.Sink(sink)
      .CreateLogger();

    var testObject = new ComplexObject
    {
      Name = "ConfigTest",
      Id = 999,
      Status = "Active",
      Description = "Should not appear"
    };

    logger.Information("Test {@Object}", testObject);

    var logEvent = sink.Events.Single();
    var objectProperty = logEvent.Properties["Object"];

    Assert.IsType<StructureValue>(objectProperty);
    var structure = (StructureValue)objectProperty;

    var propertyNames = structure.Properties.Select(p => p.Name).ToHashSet();
    Assert.Equal(2, propertyNames.Count);
    Assert.Contains("Name", propertyNames);
    Assert.Contains("Id", propertyNames);
    Assert.DoesNotContain("Status", propertyNames);
    Assert.DoesNotContain("Description", propertyNames);
  }

  [Fact]
  public void SelectiveDestructuringPolicyHandlesEmptyPropertyList()
  {
    var policy = new SelectiveDestructuringPolicy<ComplexObject>();
    var factory = new PropertyValueConverter(10, 1000, 1000, [], [], [policy], false);

    var testObject = new ComplexObject
    {
      Name = "Test",
      Id = 123
    };

    var result = factory.CreatePropertyValue(testObject, destructureObjects: true);

    Assert.IsType<StructureValue>(result);
    var structure = (StructureValue)result;

    // Should return empty structure when no properties are selected
    Assert.Empty(structure.Properties);
  }
}