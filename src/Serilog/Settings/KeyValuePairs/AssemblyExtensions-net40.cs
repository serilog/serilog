using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serilog.Settings.KeyValuePairs
{
    /// <summary>
    /// Backport of .Net 4.5 features 
    /// </summary>
    static class AssemblyExtensions
    {
        public static IEnumerable<Type> ExportedTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes();
        }
    }
}