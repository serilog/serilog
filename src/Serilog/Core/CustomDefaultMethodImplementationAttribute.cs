#if FEATURE_DEFAULT_INTERFACE

namespace Serilog.Core;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
sealed class CustomDefaultMethodImplementationAttribute : Attribute
{
}

#endif
