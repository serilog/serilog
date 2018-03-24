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
            var bytes = value as byte[];
            if (bytes == null)
            {
                result = null;
                return false;
            }

            if (bytes.Length > MaximumByteArrayLength)
            {
                var start = string.Concat(bytes.Take(16).Select(b => b.ToString("X2")));
                var description = start + "... (" + bytes.Length + " bytes)";
                result = new ScalarValue(description);
            }
            else
            {
                result = new ScalarValue(string.Concat(bytes.Select(b => b.ToString("X2"))));
            }

            return true;
        }
    }
}
