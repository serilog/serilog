namespace Serilog.Core
{
#if FEATURE_DEFAULT_INTERFACE
    using System;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    internal sealed class CustomDefaultMethodImplementationAttribute : Attribute
    {
    }
#endif
}
