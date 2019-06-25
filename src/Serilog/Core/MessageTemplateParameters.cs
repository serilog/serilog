using System;
using System.Runtime.CompilerServices;
using Serilog.Capturing;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Core
{
    readonly struct SingleValue<TValue> : IMessageTemplateParameters
    {
        readonly TValue _value;

        public SingleValue(TValue value)
        {
            _value = value;
        }

        public int Length => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return index == 0 ? converter.CreatePropertyValue(_value) : ThrowOutOfRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return index == 0 ? converter.CreatePropertyValue(_value, destructuring) : ThrowOutOfRange();
        }

        static LogEventPropertyValue ThrowOutOfRange() => throw new IndexOutOfRangeException();
    }

    readonly struct TwoValues<TValue0, TValue1> : IMessageTemplateParameters
    {
        readonly TValue0 _value0;
        readonly TValue1 _value1;

        public TwoValues(TValue0 value0, TValue1 value1)
        {
            _value0 = value0;
            _value1 = value1;
        }

        public int Length => 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return index == 0 ? converter.CreatePropertyValue(_value0) : index == 1 ? converter.CreatePropertyValue(_value1) : ThrowOutOfRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return index == 0 ? converter.CreatePropertyValue(_value0, destructuring) : index == 1 ? converter.CreatePropertyValue(_value1, destructuring) : ThrowOutOfRange();
        }

        static LogEventPropertyValue ThrowOutOfRange() => throw new IndexOutOfRangeException();
    }

    readonly struct ThreeValues<TValue0, TValue1, TValue2> : IMessageTemplateParameters
    {
        readonly TValue0 _value0;
        readonly TValue1 _value1;
        readonly TValue2 _value2;

        public ThreeValues(TValue0 value0, TValue1 value1, TValue2 value2)
        {
            _value0 = value0;
            _value1 = value1;
            _value2 = value2;
        }

        public int Length => 3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            switch (index)
            {
                case 0:
                    return converter.CreatePropertyValue(_value0);
                case 1:
                    return converter.CreatePropertyValue(_value1);
                case 2:
                    return converter.CreatePropertyValue(_value2);
                default:
                    return ThrowOutOfRange();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            switch (index)
            {
                case 0:
                    return converter.CreatePropertyValue(_value0, destructuring);
                case 1:
                    return converter.CreatePropertyValue(_value1, destructuring);
                case 2:
                    return converter.CreatePropertyValue(_value2, destructuring);
                default:
                    return ThrowOutOfRange();
            }
        }

        static LogEventPropertyValue ThrowOutOfRange() => throw new IndexOutOfRangeException();

    }

    readonly struct ObjectParameterValueProvider : IMessageTemplateParameters
    {
        readonly object[] values;

        static readonly object[] Empty = new object[0];

        public ObjectParameterValueProvider(object[] values)
        {
            this.values = values ?? Empty;
        }

        public int Length => values.Length;

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return converter.CreatePropertyValue(values[index]);
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return converter.CreatePropertyValue(values[index], destructuring);
        }
    }

    interface IMessageTemplateParameters
    {
        int Length { get; }

        LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index);

        LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring);
    }
}
