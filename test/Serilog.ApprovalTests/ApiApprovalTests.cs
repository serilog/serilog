using PublicApiGenerator;
using Serilog;
using Shouldly;
using Xunit;

public class ApiApprovalTests
{
    [Fact]
    public void PublicApi_Should_Not_Change_Unintentionally()
    {
        var assembly = typeof(ILogger).Assembly;
        var publicApi = assembly.GeneratePublicApi(
            new()
            {
                IncludeAssemblyAttributes = false,
                ExcludeAttributes = new[] { "System.Diagnostics.DebuggerDisplayAttribute" },
            });

        publicApi.ShouldMatchApproved(options =>
        {
            // Comment this line out to view the failure as a diff. Leave it here so that CI builds don't hang when this test fails.
            options.NoDiff();
            options.WithFilenameGenerator((_, _, fileType, fileExtension) => $"{assembly.GetName().Name!}.{fileType}.{fileExtension}");
        });
    }
}
