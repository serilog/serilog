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

using Serilog.Capturing;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Tests.Support;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Serilog.Tests.Events;

public class LogEventPropertyValueTests
{
    readonly PropertyValueConverter _converter =
        new(10, 1000, 1000, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>(), false);

    [Fact]
    public void AnEnumIsConvertedToANonStringScalarValue()
    {
        var value = _converter.CreatePropertyValue(LogEventLevel.Debug, Destructuring.Default);
        Assert.IsType<ScalarValue>(value);
        var sv = (ScalarValue)value;
        Assert.NotNull(sv.Value);
        Assert.IsType<LogEventLevel>(sv.Value);
    }

    [Fact]
    public void AScalarValueToStringRendersTheValue()
    {
        var num = Some.Int();
        var value = _converter.CreatePropertyValue(num, Destructuring.Default);
        var str = value.ToString();
        Assert.Equal(num.ToString(CultureInfo.InvariantCulture), str);
    }

    [Fact]
    public void AScalarValueToStringRendersTheValueUsingFormat()
    {
        var num = Some.Decimal();
        var value = _converter.CreatePropertyValue(num, Destructuring.Default);
        var str = value.ToString("N2", null);
        Assert.Equal(num.ToString("N2", CultureInfo.InvariantCulture), str);
    }

    [Fact]
    public void AScalarValueToStringRendersTheValueUsingFormatProvider()
    {
        var num = Some.Decimal();
        var value = _converter.CreatePropertyValue(num, Destructuring.Default);
        var str = value.ToString(null, new CultureInfo("fr-FR"));
        Assert.Equal(num.ToString(new CultureInfo("fr-FR")), str);
    }

    [Fact]
    public void WhenDestructuringAKnownLiteralTypeIsScalar()
    {
        var guid = Guid.NewGuid();
        var value = _converter.CreatePropertyValue(guid, Destructuring.Destructure);
        var str = value.ToString();
        Assert.Equal(guid.ToString(), str);
    }
}
