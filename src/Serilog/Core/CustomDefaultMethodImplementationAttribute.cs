#if FEATURE_DEFAULT_INTERFACE

using System;

namespace Serilog.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface)]
    sealed class CustomDefaultMethodImplementationAttribute : Attribute
    {
    }
}

#endif
