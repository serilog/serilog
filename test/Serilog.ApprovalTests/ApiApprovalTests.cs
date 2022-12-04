using PublicApiGenerator;
using Shouldly;
using Xunit;

namespace Serilog.ApprovalTests;

/// <summary>Tests for checking changes to the public API.</summary>
/// <see href="https://github.com/JakeGinnivan/ApiApprover"/>
public class ApiApprovalTests
{
    /// <summary> Check for changes to the public APIs. </summary>
    /// <param name="type"> The type used as a marker for the assembly whose public API change you want to check. </param>
    [Theory]
    [InlineData(typeof(ILogger))]
    public void PublicApi_Should_Not_Change_Unintentionally(Type type)
    {
        string publicApi = type.Assembly.GeneratePublicApi(new ApiGeneratorOptions
        {
            IncludeAssemblyAttributes = false,
            ExcludeAttributes = new[] { "System.Diagnostics.DebuggerDisplayAttribute" },
        });

        publicApi.ShouldMatchApproved(options => options!.WithFilenameGenerator((testMethodInfo, discriminator, fileType, fileExtension) => $"{type.Assembly.GetName().Name!}.{fileType}.{fileExtension}"));
    }
}
