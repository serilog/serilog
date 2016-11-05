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
using System.Text;

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

		[Theory]
		[InlineData(LogEventLevel.Verbose)]
		[InlineData(LogEventLevel.Debug)]
		[InlineData(LogEventLevel.Information)]
		[InlineData(LogEventLevel.Warning)]
		[InlineData(LogEventLevel.Error)]
		[InlineData(LogEventLevel.Fatal)]
		public void ValidateConventionForMethodSet(LogEventLevel level)
		{
			var setName = level.ToString();

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

				var report = new StringBuilder();

				foreach (var testMethod in testMethods)
				{
					try
					{
						testMethod.Invoke(this, new object[] { method, level });

						signatureMatch = true;

						break;
					}
					catch (Exception e)
						when (e is TargetInvocationException
							&& (e as TargetInvocationException).GetBaseException() is XunitException)
					{
						var xunitException = (XunitException)(e as TargetInvocationException).GetBaseException();

						if (xunitException.Data.Contains("IsSignatureAssertionFailure"))
						{
							report.AppendLine($"{testMethod.Name} Signature Mismatch on: {method} with: {xunitException.Message}");
						}
						else
						{
							report.AppendLine($"{testMethod.Name} Invocation Failure on: {method} with: {xunitException.UserMessage}");
						}

						continue;
					}
				}

				Assert.True(signatureMatch, $"{method} did not match any known convention\n" + report.ToString());
			}
		}

		// Method0 (string messageTemplate) : void
		void ValidateMethod0(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, expectedArgCount: 1);

			GetLoggerAndInvoke(method, level, "message");
		}

		// Method1<T> (string messageTemplate, T propertyValue) : void
		void ValidateMethod1(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 2);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string) }, "message", "value0");
		}

		// Method2<T0, T1> (string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		void ValidateMethod2(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string), typeof(string) },
				"message", "value0", "value1");
		}

		// Method3<T0, T1, T2> (string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		void ValidateMethod3(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string), typeof(string), typeof(string) },
				"message", "value0", "value1", "value2");
		}

		// Method4 (string messageTemplate, params object[] propertyValues) : void
		void ValidateMethod4(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, expectedArgCount: 2);

			GetLoggerAndInvoke(method, level, "message", new object[] { "value0", "value1", "value2" });
		}

		// Method5 (Exception exception, string messageTemplate) : void
		void ValidateMethod5(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 2);

			GetLoggerAndInvoke(method, level, new Exception("test"), "message");
		}

		// Method6<T> (Exception exception, string messageTemplate, T propertyValue) : void
		void ValidateMethod6(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string) }, new Exception("test"), "message", "value0");
		}

		// Method7<T0, T1> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		void ValidateMethod7(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1");
		}

		// Method8<T0, T1, T2> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		void ValidateMethod8(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 5);

			GetLoggerAndInvokeGeneric(method, level, new Type[] { typeof(string), typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1", "value2");
		}

		// Method9 (Exception exception, string messageTemplate, params object[] propertyValues) : void
		void ValidateMethod9(MethodInfo method, LogEventLevel level)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 3);

			GetLoggerAndInvoke(method, level, new Exception("test"), "message", new object[] { "value0", "value1", "value2" });
		}

		static void GetLoggerAndInvoke(MethodInfo method, LogEventLevel level, params object[] parameters)
		{
			var sink = new CollectingSink();

			var logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Sink(sink)
				.CreateLogger();

			method.Invoke(logger, parameters);

			Assert.Equal(1, sink.Events.Count);

			var evt = sink.Events.Single();

			Assert.Equal(level, evt.Level);
		}

		static void GetLoggerAndInvokeGeneric(MethodInfo method, LogEventLevel level, Type[] typeArgs, params object[] parameters)
		{
			var sink = new CollectingSink();

			var logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Sink(sink)
				.CreateLogger();

			method.MakeGenericMethod(typeArgs).Invoke(logger, parameters);

			Assert.Equal(1, sink.Events.Count);

			var evt = sink.Events.Single();

			Assert.Equal(level, evt.Level);
		}

		// parameters will always be ordered so single evaluation method will work
		static void VerifyMethodSignature(MethodInfo method, bool hasExceptionArg = false, bool isGeneric = false, int expectedArgCount = 1)
		{
			try
			{
				var parameters = method.GetParameters();

				Assert.Equal(parameters.Length, expectedArgCount);

				int index = 0;

				// exceptions always come before messageTemplate string
				if (hasExceptionArg) //verify exception argument type and name
				{
					Assert.Equal(parameters[index].ParameterType, typeof(Exception));

					Assert.Equal(parameters[index].Name, "exception");

					index++;
				}

				//check for message template string argument
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

				//check for params argument: params object[] propertyValues
				//params argument currently has to be the last argument, and generic methods don't have params argument
				if (!isGeneric && (parameters.Length - index) == 1)
				{
					var paramsArrayArg = parameters[index];

					// params array attribute should never have derived/inherited classes
					var paramsAttr = parameters[index].GetCustomAttribute(typeof(ParamArrayAttribute), inherit: false);

					Assert.NotNull(paramsAttr);

					Assert.Equal(paramsArrayArg.ParameterType, typeof(object[]));

					Assert.Equal(paramsArrayArg.Name, "propertyValues");
				}
			}
			catch (XunitException e)
			{
				// mark xunit assertion failures
				e.Data.Add("IsSignatureAssertionFailure", true);

				throw e;
			}
		}
	}
}
