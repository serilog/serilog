using PublicApiGenerator;
using Serilog;
using Shouldly;
using Xunit;

public class ApiApprovalTests
{
    /// <summary> Check for changes to the public APIs. </summary>
    /// <param name="type"> The type used as a marker for the assembly whose public API change you want to check. </param>
    [Fact]
    public void PublicApi_Should_Not_Change_Unintentionally()
    {
        var assembly = typeof(ILogger).Assembly;
        var publicApi = assembly.GeneratePublicApi(
            new()
            {
                IncludeAssemblyAttributes = false,
                ExcludeAttributes = new[] {"System.Diagnostics.DebuggerDisplayAttribute"},
            });

        publicApi.ShouldMatchApproved(options => options.WithFilenameGenerator((_, _, fileType, fileExtension) => $"{assembly.GetName().Name!}.{fileType}.{fileExtension}"));
    }
}
