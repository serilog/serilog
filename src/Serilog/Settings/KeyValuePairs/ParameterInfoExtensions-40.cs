using System.Reflection;

namespace Serilog.Settings.KeyValuePairs
{
    /// <summary>
    /// Backport of .Net 4.5 features
    /// </summary>
    static class ParameterInfoExtensions
    {
        public static bool HasDefaultValue(this ParameterInfo parameterInfo)
        {
            return (parameterInfo.Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.None;
        }    
    }
}