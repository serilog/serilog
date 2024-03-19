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
                ExcludeAttributes = ["System.Diagnostics.DebuggerDisplayAttribute"],
            });

        publicApi.ShouldMatchApproved(options =>
        {
            options.WithFilenameGenerator((_, _, fileType, fileExtension) => $"{assembly.GetName().Name!}.{fileType}.{fileExtension}");
        });
    }
}
