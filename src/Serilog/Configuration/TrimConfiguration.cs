
namespace Serilog.Configuration;

internal static class TrimConfiguration
{
    /// <summary>
    /// True if compiler-generated types is treated specially by Serilog during logging. The main example
    /// of this would be anonymous types, which have a special compiler-generated form. If this switch is
    /// disabled, Serilog will not be able to destructure anonymous types, but will still be able to log
    /// them as scalar values.
    /// </summary>
    public static bool IsCompilerGeneratedCodeSupported { get; } = !AppContext.TryGetSwitch("Serilog.IsCompilerGeneratedCodeSupported", out var isEnabled) || isEnabled;
}