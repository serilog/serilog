using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serilog.Tests.Support
{
    /// <summary>
    /// Decorates a test so that ncrunch won't run it in parallel with anything else
    /// </summary>
    public class SerialAttribute : Attribute
    {
    }
}
