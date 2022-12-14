// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Serilog.Capturing;

// Performance relevant - on the hot path when creating log events from existing templates.
class PropertyBinder
{
    readonly PropertyValueConverter _valueConverter;

    static readonly EventProperty[] NoProperties = new EventProperty[0];

    public PropertyBinder(PropertyValueConverter valueConverter)
    {
        _valueConverter = valueConverter;
    }

    /// <summary>
    /// Create properties based on an ordered list of provided values.
    /// </summary>
    /// <param name="messageTemplate">The template that the parameters apply to.</param>
    /// <param name="messageTemplateParameters">Objects corresponding to the properties
    /// represented in the message template.</param>
    /// <param name="length">The real length of the returned array, that should be used in case of pooled arrays.</param>
    /// <param name="pooled"><see langword="true"/> if the returned array is taken from the pool.</param>
    /// <returns>A list of properties; if the template is malformed then
    /// this will be empty.</returns>
    public EventProperty[] ConstructProperties(MessageTemplate messageTemplate, object?[]? messageTemplateParameters, out int length, out bool pooled)
    {
        if (messageTemplateParameters == null || messageTemplateParameters.Length == 0)
        {
            if (messageTemplate.NamedProperties != null || messageTemplate.PositionalProperties != null)
                SelfLog.WriteLine("Required properties not provided for: {0}", messageTemplate);

            length = 0;
            pooled = false;
            return NoProperties;
        }

        if (messageTemplate.PositionalProperties != null)
        {
            var array1 = ConstructPositionalProperties(messageTemplate, messageTemplateParameters, messageTemplate.PositionalProperties);
            length = array1.Length;
            pooled = false;
            return array1;
        }

        return ConstructNamedProperties(messageTemplate, messageTemplateParameters!, out length, out pooled);
    }

    EventProperty[] ConstructPositionalProperties(MessageTemplate template, object?[] messageTemplateParameters, PropertyToken[] positionalProperties)
    {
        if (positionalProperties.Length != messageTemplateParameters.Length)
            SelfLog.WriteLine("Positional property count does not match parameter count: {0}", template);

        var result = new EventProperty[messageTemplateParameters.Length];
        foreach (var property in positionalProperties)
        {
            if (property.TryGetPositionalValue(out var position))
            {
                if (position < 0 || position >= messageTemplateParameters.Length)
                    SelfLog.WriteLine("Unassigned positional value {0} in: {1}", position, template);
                else
                    result[position] = ConstructProperty(property, messageTemplateParameters[position]);
            }
        }

        var next = 0;
        for (var i = 0; i < result.Length; ++i)
        {
            if (!result[i].Equals(EventProperty.None))
            {
                result[next] = result[i];
                ++next;
            }
        }

        if (next != result.Length)
            Array.Resize(ref result, next);

        return result;
    }

#if FEATURE_ARRAYPOOL
    private readonly static System.Buffers.ArrayPool<EventProperty> _eventPropertyPool = System.Buffers.ArrayPool<EventProperty>.Shared;
#endif

    internal static void Return(EventProperty[] array)
    {
#if FEATURE_ARRAYPOOL
        _eventPropertyPool.Return(array, clearArray: true);
#endif
    }

    EventProperty[] ConstructNamedProperties(MessageTemplate template, object[] messageTemplateParameters, out int length, out bool pooled)
    {
        var namedProperties = template.NamedProperties;
        if (namedProperties == null)
        {
            length = 0;
            pooled = false;
            return NoProperties;
        }

        var matchedRun = namedProperties.Length;
        if (namedProperties.Length != messageTemplateParameters.Length)
        {
            matchedRun = Math.Min(namedProperties.Length, messageTemplateParameters.Length);
            SelfLog.WriteLine("Named property count does not match parameter count: {0}", template);
        }

#if FEATURE_ARRAYPOOL
        var result = _eventPropertyPool.Rent(messageTemplateParameters.Length);
        pooled = true;
#else
        var result = new EventProperty[messageTemplateParameters.Length];
        pooled = false;
#endif
        for (var i = 0; i < matchedRun; ++i)
        {
            var property = namedProperties[i];
            var value = messageTemplateParameters[i];
            result[i] = ConstructProperty(property, value);
        }

        for (var i = matchedRun; i < messageTemplateParameters.Length; ++i)
        {
            var value = _valueConverter.CreatePropertyValue(messageTemplateParameters[i]);
            result[i] = new("__" + i, value);
        }
        length = messageTemplateParameters.Length;
        return result;
    }

    EventProperty ConstructProperty(PropertyToken propertyToken, object? value)
    {
        return new(
            propertyToken.PropertyName,
            _valueConverter.CreatePropertyValue(value, propertyToken.Destructuring));
    }
}
