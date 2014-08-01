using System;

namespace Serilog.Reflection
{
    class PropertyAccessor
    {
        public string Name;
        public Func<object, object> GetDelegate;
    }
}