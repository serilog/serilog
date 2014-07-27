namespace Serilog.Parameters
{
    using System;

    class PropertyAccessor
    {
        public string Name;
        public Func<object, object> GetDelegate;
    }
}