// Copyright 2013-2017 Serilog Contributors
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

using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Policies
{
    // Byte arrays, when logged, need to be copied so that they are
    // safe from concurrent modification when written to asynchronous
    // sinks. Byte arrays larger than 1k are written as descriptive strings.
    class ByteArrayScalarConversionPolicy : IScalarConversionPolicy
    {
        const int MaximumByteArrayLength = 1024;

        public bool TryConvertToScalar(object value, out ScalarValue result)
        {
            if (value.GetType() != typeof(byte[]))
            {
                result = null;
                return false;
            }

            var bytes = value as byte[];
            if (bytes == null)
            {
                result = null;
                return false;
            }

            result = ConvertToScalarValue(bytes);

            return true;
        }

        public static ScalarValue ConvertToScalarValue(byte[] bytes)
        {
            if (bytes.Length > MaximumByteArrayLength)
            {
                var prefix = bytes.Take(16).ToArray();
                var start = ByteArrayToHexViaLookup32(prefix);
                var description = start + "... (" + bytes.Length + " bytes)";
                return new ScalarValue(description);
            }

            return new ScalarValue(ByteArrayToHexViaLookup32(bytes));
        }

        static readonly uint[] _lookup32 = CreateLookup32();

        static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s = i.ToString("X2");
                result[i] = s[0] + ((uint)s[1] << 16);
            }
            return result;
        }

        static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }
    }
}
