using System;

namespace Serilog.Core
{
    readonly struct SingleValue<TValue> : IMessageTemplateParameters
    {
        public readonly TValue Value;

        public SingleValue(TValue value)
        {
            Value = value;
        }

        public int Length => 1;

        public object this[int index] => index == 0 ? Value : throw new IndexOutOfRangeException();
    }

    readonly struct MultiValue<TValue, TNext> : IMessageTemplateParameters
        where TNext : IMessageTemplateParameters
    {
        public readonly TValue Value;
        public readonly TNext Next;

        public MultiValue(TValue value, TNext next)
        {
            Value = value;
            Next = next;
        }

        public int Length => Next.Length + 1;

        public object this[int index] => index == 0 ? Value : Next[index - 1];
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

        public object this[int index] => values[index];
    }

    interface IMessageTemplateParameters
    {
        int Length { get; }

        // TODO: temporarily, before it's not pushed down
        object this[int index] { get; }
    }
}
