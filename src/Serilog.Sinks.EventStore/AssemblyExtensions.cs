using System;
using System.IO;
using System.Reflection;

namespace Serilog
{
    /// <summary>
    /// Class containing methids extending the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Retrieve the executing filder of the <see cref="Assembly"/> passed in.
        /// </summary>
        /// <param name="asm">The assembly who's folder you wish to retrieve.</param>
        /// <returns>The assemblies executing folder.</returns>
        public static string GetExecutingFolder(this Assembly asm)
        {
            if (asm == null)
            {
                throw new ArgumentNullException("The assembly passed in is null.");
            }
            
            return Path.GetDirectoryName(new Uri(asm.CodeBase).LocalPath);
        }
    }
}
