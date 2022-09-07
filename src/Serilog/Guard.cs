using JetBrains.Annotations;
#if !NET5_0_OR_GREATER

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter)]
    sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public CallerArgumentExpressionAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}
#endif

namespace JetBrains.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    sealed class NoEnumerationAttribute : Attribute
    {
    }
}

static class Guard
{
    public static T AgainstNull<T>(
        [NoEnumeration][NotNull] T? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
        where T : class
    {
        if (argument is null)
        {
            throw new ArgumentNullException(paramName);
        }

        return argument;
    }
}
