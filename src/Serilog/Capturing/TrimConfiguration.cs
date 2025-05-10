
namespace Serilog.Capturing;

static class TrimConfiguration
{
    /// <summary>
    /// True if structure-value types are treated specially by Serilog during logging. The main example
    /// of this would be anonymous types, which have a special compiler-generated form. If this switch is
    /// disabled, Serilog will not be able to destructure anonymous types, but will still be able to log
    /// them as scalar values.
    /// </summary>
    public static bool IsStructureValueSupported { get; } =
        !AppContext.TryGetSwitch("Serilog.Capturing.IsStructureValueSupported", out var isEnabled) || isEnabled;
}