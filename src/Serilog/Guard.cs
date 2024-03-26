using JetBrains.Annotations;

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
