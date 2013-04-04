// Copyright 2013 Nicholas Blumhardt
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

using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;
using Serilog.Parsing;
using Serilog.Tests.Support;

namespace Serilog.Tests.Events
{
    [TestFixture]
    public class LogEventPropertyValueTests
    {
        readonly PropertyValueConverter _converter = new PropertyValueConverter(Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>());

        [Test]
        public void AnEnumIsConvertedToANonStringScalarValue()
        {
            var value = _converter.CreatePropertyValue(LogEventLevel.Debug, Destructuring.Default);
            Assert.IsInstanceOf<ScalarValue>(value);
            var sv = (ScalarValue)value;
            Assert.IsNotNull(sv.Value);
            Assert.IsInstanceOf<LogEventLevel>(sv.Value);
        }

        [Test]
        public void AScalarValueToStringRendersTheValue()
        {
            var num = Some.Int();
            var value = _converter.CreatePropertyValue(num, Destructuring.Default);
            var str = value.ToString();
            Assert.AreEqual(num.ToString(CultureInfo.InvariantCulture), str);
        }

        [Test]
        public void WhenDestructuringAKnownLiteralTypeIsScalar()
        {
            var guid = Guid.NewGuid();
            var value = _converter.CreatePropertyValue(guid, Destructuring.Destructure);
            var str = value.ToString();
            Assert.AreEqual(guid.ToString(), str);
        }
    }
}
