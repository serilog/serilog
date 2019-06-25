using System;
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
            this._value = value;
        }

        public int Length => 1;

        object this[int index] => index == 0 ? _value : throw new IndexOutOfRangeException();

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return converter.CreatePropertyValue(this[index]);
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return converter.CreatePropertyValue(this[index], destructuring);
        }
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

        object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return _value0;
                    case 1:
                        return _value1;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return converter.CreatePropertyValue(this[index]);
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return converter.CreatePropertyValue(this[index], destructuring);
        }
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

        object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return _value0;
                    case 1:
                        return _value1;
                    case 2:
                        return _value2;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index)
        {
            return converter.CreatePropertyValue(this[index]);
        }

        public LogEventPropertyValue CreatePropertyValue(PropertyValueConverter converter, int index, Destructuring destructuring)
        {
            return converter.CreatePropertyValue(this[index], destructuring);
        }
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
