using System;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Serilog.Settings.KeyValuePairs;
using Serilog.Tests.Support;
using Xunit;

namespace Serilog.Tests.Settings
{
    public class SettingValueConversionsTests
    {
        [Fact]
        public void ConvertibleValuesConvertToTIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType("3", typeof(int?));
            Assert.True(result == 3);
        }

        [Fact]
        public void NullValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType(null, typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void EmptyStringValuesConvertToNullIfTargetIsNullable()
        {
            var result = (int?)SettingValueConversions.ConvertToType("", typeof(int?));
            Assert.True(result == null);
        }

        [Fact]
        public void ValuesConvertToNullableTimeSpan()
        {
            var result = (TimeSpan?)SettingValueConversions.ConvertToType("00:01:00", typeof(TimeSpan?));
            Assert.Equal(TimeSpan.FromMinutes(1), result);
        }

        [Fact]
        public void ValuesConvertToEnumMembers()
        {
            var result = (LogEventLevel)SettingValueConversions.ConvertToType("Information", typeof(LogEventLevel));
            Assert.Equal(LogEventLevel.Information, result);
        }

        [Fact]
        public void ValuesConvertToStringArray()
        {
            var result = (string[])SettingValueConversions.ConvertToType("key1=value1,key2=value2", typeof(string[]));
            Assert.Equal(2, result.Length);
            Assert.Equal("key1=value1", result[0]);
            Assert.Equal("key2=value2", result[1]);
        }

        [Fact]
        public void ValuesConvertToStringArrayEmpty()
        {
            var result = (string[])SettingValueConversions.ConvertToType("", typeof(string[]));
            Assert.Empty(result);
        }

        [Fact]
        public void ValuesConvertToTypeFromQualifiedName()
        {
            var result = (Type)SettingValueConversions.ConvertToType("System.Version", typeof(Type));
            Assert.Equal(typeof(Version), result);
        }

        [Fact]
        public void ValuesConvertToTypeFromAssemblyQualifiedName()
        {
            var assemblyQualifiedName = typeof(Version).AssemblyQualifiedName;
            var result = (Type)SettingValueConversions.ConvertToType(assemblyQualifiedName, typeof(Type));
            Assert.Equal(typeof(Version), result);
        }

        [Fact]
        public void StringValuesConvertToDefaultInstancesIfTargetIsInterface()
        {
            var result = SettingValueConversions.ConvertToType("Serilog.Formatting.Json.JsonFormatter", typeof(ITextFormatter));
            Assert.IsType<JsonFormatter>(result);
        }

        [Fact]
        public void StringValuesConvertToDefaultInstancesIfTargetIsAbstractClass()
        {
            var result = SettingValueConversions.ConvertToType("Serilog.Tests.Support.DummyConcreteClassWithDefaultConstructor, Serilog.Tests", typeof(DummyAbstractClass));
            Assert.IsType<DummyConcreteClassWithDefaultConstructor>(result);
        }

        [Fact]
        public void StringValuesThrowsWhenMissingDefaultConstructorIfTargetIsAbstractClass()
        {
            var value = "Serilog.Tests.Support.DummyConcreteClassWithoutDefaultConstructor, Serilog.Tests";
            Assert.Throws<InvalidOperationException>(() =>
                SettingValueConversions.ConvertToType(value, typeof(DummyAbstractClass)));
        }

        [Theory]
        [InlineData("3.14:21:18.986", 3 /*days*/, 14 /*hours*/, 21 /*min*/, 18 /*sec*/, 986 /*ms*/)]
        [InlineData("4", 4, 0, 0, 0, 0)] // minimal : days
        [InlineData("2:0", 0, 2, 0, 0, 0)] // minimal hours
        [InlineData("0:5", 0, 0, 5, 0, 0)] // minimal minutes
        [InlineData("0:0:7", 0, 0, 0, 7, 0)] // minimal seconds
        [InlineData("0:0:0.2", 0, 0, 0, 0, 200)] // minimal milliseconds
        public void TimeSpanValuesCanBeParsed(string input, int expDays, int expHours, int expMin, int expSec, int expMs)
        {
            var expectedTimeSpan = new TimeSpan(expDays, expHours, expMin, expSec, expMs);
            var actual = SettingValueConversions.ConvertToType(input, typeof(TimeSpan));

            Assert.IsType<TimeSpan>(actual);
            Assert.Equal(expectedTimeSpan, actual);
        }

        [Theory]
        [InlineData("My.NameSpace.Class+InnerClass::Member",
                    "My.NameSpace.Class+InnerClass", "Member")]
        [InlineData("  TrimMe.NameSpace.Class::NeedsTrimming  ",
                    "TrimMe.NameSpace.Class", "NeedsTrimming")]
        [InlineData("My.NameSpace.Class::Member",
                    "My.NameSpace.Class", "Member")]
        [InlineData("My.NameSpace.Class::Member, MyAssembly",
                    "My.NameSpace.Class, MyAssembly", "Member")]
        [InlineData("My.NameSpace.Class::Member, MyAssembly, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                    "My.NameSpace.Class, MyAssembly, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "Member")]
        [InlineData("Just a random string with :: in it",
                    null, null)]
        [InlineData("Its::a::trapWithColonsAppearingTwice",
                    null, null)]
        [InlineData("ThereIsNoMemberHere::",
                    null, null)]
        [InlineData(null,
                    null, null)]
        [InlineData(" ",
                    null, null)]
        // a full-qualified type name should not be considered a static member accessor
        [InlineData("My.NameSpace.Class, MyAssembly, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            null, null)]
        public void TryParseStaticMemberAccessorReturnsExpectedResults(string input, string expectedAccessorType, string expectedPropertyName)
        {
            var actual = SettingValueConversions.TryParseStaticMemberAccessor(input,
                out var actualAccessorType,
                out var actualMemberName);

            if (expectedAccessorType == null)
            {
                Assert.False(actual, $"Should not parse {input}");
            }
            else
            {
                Assert.True(actual, $"should successfully parse {input}");
                Assert.Equal(expectedAccessorType, actualAccessorType);
                Assert.Equal(expectedPropertyName, actualMemberName);
            }
        }

        [Theory]
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InterfaceProperty, Serilog.Tests", typeof(IAmAnInterface))]
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::AbstractProperty, Serilog.Tests", typeof(AnAbstractClass))]
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InterfaceField, Serilog.Tests", typeof(IAmAnInterface))]
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::AbstractField, Serilog.Tests", typeof(AnAbstractClass))]
        public void StaticMembersAccessorsCanBeUsedForReferenceTypes(string input, Type targetType)
        {
            var actual = SettingValueConversions.ConvertToType(input, targetType);

            Assert.IsAssignableFrom(targetType, actual);
            Assert.Equal(ConcreteImpl.Instance, actual);
        }

        [Theory]
        // unknown type
        [InlineData("Namespace.ThisIsNotAKnownType::InterfaceProperty, Serilog.Tests", typeof(IAmAnInterface))]
        // good type name, but wrong namespace
        [InlineData("Random.Namespace.ClassWithStaticAccessors::InterfaceProperty, Serilog.Tests", typeof(IAmAnInterface))]
        // good full type name, but missing or wrong assembly
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InterfaceProperty", typeof(IAmAnInterface))]
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InterfaceProperty, TestDummies", typeof(IAmAnInterface))]
        public void StaticAccessorOnUnknownTypeThrowsTypeLoadException(string input, Type targetType)
        {
            Assert.Throws<TypeLoadException>(() =>
                SettingValueConversions.ConvertToType(input, targetType)
            );
        }

        [Theory]
        // unknown member
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::UnknownMember, Serilog.Tests", typeof(IAmAnInterface))]
        // static property exists but it's private
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::PrivateInterfaceProperty, Serilog.Tests", typeof(IAmAnInterface))]
        // static field exists but it's private
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::PrivateInterfaceField, Serilog.Tests", typeof(IAmAnInterface))]
        // public property exists but it's not static
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InstanceInterfaceProperty, Serilog.Tests", typeof(IAmAnInterface))]
        // public field exists but it's not static
        [InlineData("Serilog.Tests.Support.ClassWithStaticAccessors::InstanceInterfaceField, Serilog.Tests", typeof(IAmAnInterface))]
        public void StaticAccessorWithInvalidMemberThrowsInvalidOperationException(string input, Type targetType)
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
                SettingValueConversions.ConvertToType(input, targetType)
            );

            Assert.Contains("Could not find a public static property or field ", exception.Message);
            Assert.Contains("on type `Serilog.Tests.Support.ClassWithStaticAccessors, Serilog.Tests`", exception.Message);
        }
    }
}
