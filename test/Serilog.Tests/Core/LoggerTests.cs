using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using System.Reflection;
using Xunit.Sdk;
using System.Text.RegularExpressions;

namespace Serilog.Tests.Core
{
    public class LoggerTests
    {
        [Fact]
        public void AnExceptionThrownByAnEnricherIsNotPropagated()
        {
            var thrown = false;

            var l = new LoggerConfiguration()
                .WriteTo.Sink(new StringSink())
                .Enrich.With(new DelegatingEnricher((le, pf) =>
                {
                    thrown = true;
                    throw new Exception("No go, pal.");
                }))
                .CreateLogger();

            l.Information(Some.String());

            Assert.True(thrown);
        }

        [Fact]
        public void AContextualLoggerAddsTheSourceTypeName()
        {
            var evt = DelegatingSink.GetLogEvent(l => l.ForContext<LoggerTests>()
                .Information(Some.String()));

            var lv = evt.Properties[Constants.SourceContextPropertyName].LiteralValue();
            Assert.Equal(typeof(LoggerTests).FullName, lv);
        }

        [Fact]
        public void PropertiesInANestedContextOverrideParentContextValues()
        {
            var name = Some.String();
            var v1 = Some.Int();
            var v2 = Some.Int();
            var evt = DelegatingSink.GetLogEvent(l => l.ForContext(name, v1)
                .ForContext(name, v2)
                .Write(Some.InformationEvent()));

            var pActual = evt.Properties[name];
            Assert.Equal(v2, pActual.LiteralValue());
        }

        [Fact]
        public void ParametersForAnEmptyTemplateAreIgnored()
        {
            var e = DelegatingSink.GetLogEvent(l => l.Error("message", new object()));
            Assert.Equal("message", e.RenderMessage());
        }

        [Fact]
        public void LoggingLevelSwitchDynamicallyChangesLevel()
        {
            var events = new List<LogEvent>();
            var sink = new DelegatingSink(events.Add);

            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);

            var log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Sink(sink)
                .CreateLogger()
                .ForContext<LoggerTests>();

            log.Debug("Suppressed");
            log.Information("Emitted");
            log.Warning("Emitted");

            // Change the level
            levelSwitch.MinimumLevel = LogEventLevel.Error;

            log.Warning("Suppressed");
            log.Error("Emitted");
            log.Fatal("Emitted");

            Assert.Equal(4, events.Count);
            Assert.True(events.All(evt => evt.RenderMessage() == "Emitted"));
        }

        [Fact]
        public void MessageTemplatesCanBeBound()
        {
            var log = new LoggerConfiguration()
                .CreateLogger();

            MessageTemplate template;
            IEnumerable<LogEventProperty> properties;
            Assert.True(log.BindMessageTemplate("Hello, {Name}!", new object[] { "World" }, out template, out properties));

            Assert.Equal("Hello, {Name}!", template.Text);
            Assert.Equal("World", properties.Single().Value.LiteralValue());
        }

        [Fact]
        public void PropertiesCanBeBound()
        {
            var log = new LoggerConfiguration()
                .CreateLogger();

            LogEventProperty property;
            Assert.True(log.BindProperty("Name", "World", false, out property));

            Assert.Equal("Name", property.Name);
            Assert.Equal("World", property.Value.LiteralValue());
        }

		[Fact]
		public void VerboseMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Verbose");
		}

		[Fact]
		public void DebugMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Debug");
		}

		[Fact]
		public void InformationMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Information");
		}

		[Fact]
		public void WarningMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Warning");
		}

		[Fact]
		public void ErrorMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Error");
		}

		[Fact]
		public void FatalMethodsMatchConvention()
		{
			ValidateConventionForMethodSet("Fatal");
		}

		private void ValidateConventionForMethodSet(string setName)
		{
			var methodSet = typeof(Logger).GetMethods().Where(method => method.Name == setName);

			var testMethods = typeof(LoggerTests).GetRuntimeMethods()
				.Where(method => Regex.IsMatch(method.Name, "ValidateMethod\\d"));

			foreach (var method in methodSet)
			{
				Assert.Equal(method.ReturnType, typeof(void));

				Assert.True(method.IsPublic);

				var messageTemplateAttr = method.GetCustomAttribute<MessageTemplateFormatMethodAttribute>();

				Assert.NotNull(messageTemplateAttr);

				Assert.Equal(messageTemplateAttr.MessageTemplateParameterName, "messageTemplate");

				var signatureMatch = false;

				foreach (var test in testMethods)
				{
					try
					{
						test.Invoke(this, new object[] { method });

						signatureMatch = true;

						break;
					}
					catch (Exception e)
					when (e is TargetInvocationException
							&& ((TargetInvocationException)e).GetBaseException() is XunitException)
					{
						continue;
					}
				}

				Assert.True(signatureMatch, $"{method} did not match any known convention");
			}
		}


		// Method0 (string messageTemplate) : void
		private void ValidateMethod0(MethodInfo method)
		{
			VerifyMethodSignature(method, expectedArgCount: 1);

			GetLoggerAndInvoke(method, "message");
		}

		// Method1<T> (string messageTemplate, T propertyValue) : void
		private void ValidateMethod1(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 2);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string) }, "message", "value0");
		}

		// Method2<T0, T1> (string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		private void ValidateMethod2(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string), typeof(string) },
				"message", "value0", "value1");
		}

		// Method3<T0, T1, T2> (string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		private void ValidateMethod3(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string), typeof(string), typeof(string) },
				"message", "value0", "value1", "value2");
		}

		// Method4 (string messageTemplate, params object[] propertyValues) : void
		private void ValidateMethod4(MethodInfo method)
		{
			VerifyMethodSignature(method, expectedArgCount: 2);

			GetLoggerAndInvoke(method, "message", new object[] { "value0", "value1", "value2" });
		}

		// Method5 (Exception exception, string messageTemplate) : void
		private void ValidateMethod5(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 2);

			GetLoggerAndInvoke(method, new Exception("test"), "message");
		}

		// Method6<T> (Exception exception, string messageTemplate, T propertyValue) : void
		private void ValidateMethod6(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string) }, new Exception("test"), "message", "value0");
		}

		// Method7<T0, T1> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		private void ValidateMethod7(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1");
		}

		// Method8<T0, T1, T2> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		private void ValidateMethod8(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 5);

			var typeOfString = typeof(string);

			GetLoggerAndInvokeGeneric(method, new Type[] { typeof(string), typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1", "value2");
		}

		// Method9 (Exception exception, string messageTemplate, params object[] propertyValues) : void
		private void ValidateMethod9(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 3);

			GetLoggerAndInvoke(method, new Exception("test"), "message", new object[] { "value0", "value1", "value2" });
		}

		private static void GetLoggerAndInvoke(MethodInfo method, params object[] parameters)
		{
			var logger = new LoggerConfiguration().CreateLogger();

			method.Invoke(logger, parameters);
		}

		private static void GetLoggerAndInvokeGeneric(MethodInfo method, Type[] typeArgs, params object[] parameters)
		{
			var logger = new LoggerConfiguration().CreateLogger();

			method.MakeGenericMethod(typeArgs).Invoke(logger, parameters);
		}

		// parameters will always be ordered so single evaluation method will work
		private static void VerifyMethodSignature(MethodInfo method, bool hasExceptionArg = false, bool isGeneric = false, int expectedArgCount = 1)
		{
			var parameters = method.GetParameters();

			Assert.Equal(parameters.Length, expectedArgCount);

			int index = 0;

			if (hasExceptionArg) //verify exception arg type and name
			{
				Assert.Equal(parameters[index].ParameterType, typeof(Exception));

				Assert.Equal(parameters[index].Name, "exception");

				index++;
			}

			//check message template argument
			Assert.Equal(parameters[index].ParameterType, typeof(string));

			Assert.Equal(parameters[index].Name, "messageTemplate");

			index++;

			if (isGeneric) //validate type arguments, generic parameters, and cross-reference
			{
				Assert.True(method.IsGenericMethod);

				var genericTypeArgs = method.GetGenericArguments();

				//multiple generic argument convention T0...Tx : T0 propertyValue0... Tx propertyValueX
				if (genericTypeArgs.Length > 1)
				{
					for (int i = 0; i < genericTypeArgs.Length; i++, index++)
					{
						Assert.Equal(genericTypeArgs[i].Name, $"T{i}");

						var genericConstraints = genericTypeArgs[i].GetTypeInfo().GetGenericParameterConstraints();

						Assert.Empty(genericConstraints);

						Assert.Equal(parameters[index].Name, $"propertyValue{i}");

						Assert.Equal(parameters[index].ParameterType, genericTypeArgs[i]);
					}
				}
				else //single generic argument convention T : T propertyValue
				{
					var genericTypeArg = genericTypeArgs[0];

					Assert.Equal(genericTypeArg.Name, "T");

					var genericConstraints = genericTypeArg.GetTypeInfo().GetGenericParameterConstraints();

					Assert.Empty(genericConstraints);

					Assert.Equal(parameters[index].Name, "propertyValue");

					Assert.Equal(genericTypeArg, parameters[index].ParameterType);

					index++;
				}
			}

			//check for params argument
			//params args currently have to be the last argument, and generics don't have params arg
			if (!isGeneric && (parameters.Length - index) == 1)
			{
				var paramsArrayArg = parameters[index];

				var paramsAttr = parameters[index].GetCustomAttribute(typeof(ParamArrayAttribute), inherit: false);

				Assert.NotNull(paramsAttr);

				Assert.Equal(paramsArrayArg.ParameterType, typeof(object[]));

				Assert.Equal(paramsArrayArg.Name, "propertyValues");
			}
		}
	}
}
