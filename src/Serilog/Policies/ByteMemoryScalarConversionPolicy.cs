// Copyright 2021 Serilog Contributors
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

namespace Serilog.Policies;

// Byte arrays, when logged, need to be copied so that they are
// safe from concurrent modification when written to asynchronous
// sinks. Byte arrays larger than 1k are written as descriptive strings.
sealed class ByteMemoryScalarConversionPolicy : IScalarConversionPolicy
{
    const int MaximumByteArrayLength = 1024;
    const int MaxTake = 16;

    public bool TryConvertToScalar(object value, [NotNullWhen(true)] out ScalarValue? result)
    {
        if (value is ReadOnlyMemory<byte> x)
        {
            result = new(ConvertToHexString(x));
            return true;
        }

        if (value is Memory<byte> y)
        {
            result = new(ConvertToHexString(y));
            return true;
        }

        result = null;
        return false;
    }

    static string ConvertToHexString(ReadOnlyMemory<byte> bytes)
    {
        if (bytes.Length > MaximumByteArrayLength)
        {
            return ConvertToHexString(bytes[..MaxTake], $"... ({bytes.Length} bytes)");
        }

        return ConvertToHexString(bytes, tail: "");
    }

    static string ConvertToHexString(ReadOnlyMemory<byte> src, string tail)
    {
        var stringLength = src.Length * 2 + tail.Length;

        return string.Create(stringLength, (src, tail), (dest, state) =>
        {
            var (src, tail) = state;

            var byteSpan = src.Span;
            foreach (var b in byteSpan)
            {
                if (b.TryFormat(dest, out var written, "X2"))
                {
                    dest = dest[written..];
                }
            }

            for (var i = 0; i < tail.Length; ++i)
            {
                dest[i] = tail[i];
            }
        });
    }
}
