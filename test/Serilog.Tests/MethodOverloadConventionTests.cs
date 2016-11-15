using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Serilog.Tests
{
    public class MethodOverloadConventionTests
    {
		const string Write = "Write";

		//this is used as both the variable name for message template parameter
		// and as the argument for the MessageTemplateFormatMethodAttr
		const string MessageTemplate = "messageTemplate";

		[Theory]
		[InlineData(Write)]
		[InlineData(nameof(LogEventLevel.Verbose))]
		[InlineData(nameof(LogEventLevel.Debug))]
		[InlineData(nameof(LogEventLevel.Information))]
		[InlineData(nameof(LogEventLevel.Warning))]
		[InlineData(nameof(LogEventLevel.Error))]
		[InlineData(nameof(LogEventLevel.Fatal))]
		public void ILoggerValidateConventions(string setName)
		{
			ValidateConventionForMethodSet(setName, typeof(ILogger));
		}

		[Theory]
		[InlineData(Write)]
		[InlineData(nameof(LogEventLevel.Verbose))]
		[InlineData(nameof(LogEventLevel.Debug))]
		[InlineData(nameof(LogEventLevel.Information))]
		[InlineData(nameof(LogEventLevel.Warning))]
		[InlineData(nameof(LogEventLevel.Error))]
		[InlineData(nameof(LogEventLevel.Fatal))]
		public void LoggerValidateConventions(string setName)
		{
			ValidateConventionForMethodSet(setName, typeof(Logger));
		}

		[Theory]
		[InlineData(Write)]
		[InlineData(nameof(LogEventLevel.Verbose))]
		[InlineData(nameof(LogEventLevel.Debug))]
		[InlineData(nameof(LogEventLevel.Information))]
		[InlineData(nameof(LogEventLevel.Warning))]
		[InlineData(nameof(LogEventLevel.Error))]
		[InlineData(nameof(LogEventLevel.Fatal))]
		public void LogValidateConventions(string setName)
		{
			ValidateConventionForMethodSet(setName, typeof(Log));
		}

		void ValidateConventionForMethodSet(string setName, Type loggerType)
		{
			IEnumerable<MethodInfo> methodSet;

			if (setName == Write)
				methodSet = loggerType.GetMethods()
					.Where(method => method.Name == setName && method.GetParameters()
					.Any(param => param.ParameterType == typeof(string)));
			else
				methodSet = loggerType.GetMethods()
					.Where(method => method.Name == setName);

			var testMethods = typeof(MethodOverloadConventionTests).GetRuntimeMethods()
					.Where(method => Regex.IsMatch(method.Name, "ValidateMethod\\d"));

			Assert.Equal(testMethods.Count(), methodSet.Count());

			foreach (var method in methodSet)
			{
				Assert.Equal(method.ReturnType, typeof(void));

				Assert.True(method.IsPublic);

				var messageTemplateAttr = method.GetCustomAttribute<MessageTemplateFormatMethodAttribute>();

				Assert.NotNull(messageTemplateAttr);

				Assert.Equal(messageTemplateAttr.MessageTemplateParameterName, MessageTemplate);

				var signatureMatchAndInvokeSuccess = false;

				var report = new StringBuilder();

				foreach (var testMethod in testMethods)
				{
					try
					{
						testMethod.Invoke(this, new object[] { method });

						signatureMatchAndInvokeSuccess = true;

						break;
					}
					catch (TargetInvocationException e)
						when (e.GetBaseException() is XunitException)
					{
						var xunitException = (XunitException)e.GetBaseException();

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

				Assert.True(signatureMatchAndInvokeSuccess, $"{method} did not match any known convention or failed invoke\n" + report.ToString());
			}
		}

		// Method0 (string messageTemplate) : void
		void ValidateMethod0(MethodInfo method)
		{
			VerifyMethodSignature(method, expectedArgCount: 1);

			GetLoggerAndInvoke(method, null, "message");
		}

		// Method1<T> (string messageTemplate, T propertyValue) : void
		void ValidateMethod1(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 2);

			GetLoggerAndInvoke(method,
				new Type[] { typeof(string) }, "message", "value0");
		}

		// Method2<T0, T1> (string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		void ValidateMethod2(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvoke(method,
				new Type[] { typeof(string), typeof(string) },
				"message", "value0", "value1");
		}

		// Method3<T0, T1, T2> (string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		void ValidateMethod3(MethodInfo method)
		{
			VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvoke(method,
				new Type[] { typeof(string), typeof(string), typeof(string) },
				"message", "value0", "value1", "value2");
		}

		// Method4 (string messageTemplate, params object[] propertyValues) : void
		void ValidateMethod4(MethodInfo method)
		{
			VerifyMethodSignature(method, expectedArgCount: 2);

			GetLoggerAndInvoke(method, null,
				"message", new object[] { "value0", "value1", "value2" });
		}

		// Method5 (Exception exception, string messageTemplate) : void
		void ValidateMethod5(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 2);

			GetLoggerAndInvoke(method, null,
				new Exception("test"), "message");
		}

		// Method6<T> (Exception exception, string messageTemplate, T propertyValue) : void
		void ValidateMethod6(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 3);

			GetLoggerAndInvoke(method,
				new Type[] { typeof(string) },
				new Exception("test"), "message", "value0");
		}

		// Method7<T0, T1> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
		void ValidateMethod7(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 4);

			GetLoggerAndInvoke(method, 
				new Type[] { typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1");
		}

		// Method8<T0, T1, T2> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
		void ValidateMethod8(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 5);

			GetLoggerAndInvoke(method,
				new Type[] { typeof(string), typeof(string), typeof(string) },
				new Exception("test"), "message", "value0", "value1", "value2");
		}

		// Method9 (Exception exception, string messageTemplate, params object[] propertyValues) : void
		void ValidateMethod9(MethodInfo method)
		{
			VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 3);

			GetLoggerAndInvoke(method, null,
				new Exception("test"), "message", new object[] { "value0", "value1", "value2" });
		}

		static void GetLoggerAndInvoke(MethodInfo method, Type[] typeArgs = null, params object[] parameters)
		{
			var sink = new CollectingSink();

			var logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.Sink(sink)
				.CreateLogger();

			LogEventLevel level;

			if (method.Name == Write)
			{
				level = LogEventLevel.Verbose;

				var paramList = new List<object>() { level };

				paramList.AddRange(parameters);

				parameters = paramList.ToArray();
			}
			else
				Assert.True(Enum.TryParse(method.Name, out level));

			if (method.IsStatic)
			{
				Log.Logger = logger;

				if (method.IsGenericMethod)
					method.MakeGenericMethod(typeArgs).Invoke(null, parameters);
				else
					method.Invoke(null, parameters);
			}
			else if (method.IsGenericMethod)
				method.MakeGenericMethod(typeArgs).Invoke(logger, parameters);
			else
				method.Invoke(logger, parameters);

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

				int index = 0;

				if (method.Name == Write)
				{
					//write convention methods always have one more parameter, LogEventLevel Arg
					expectedArgCount++;

					Assert.Equal(parameters[index].ParameterType, typeof(LogEventLevel));

					Assert.Equal(parameters[index].Name, "level");

					index++;
				}

				Assert.Equal(parameters.Length, expectedArgCount);

				// exceptions always come before messageTemplate string
				if (hasExceptionArg) //verify exception argument type and name
				{
					Assert.Equal(parameters[index].ParameterType, typeof(Exception));

					Assert.Equal(parameters[index].Name, "exception");

					index++;
				}

				//check for message template string argument
				Assert.Equal(parameters[index].ParameterType, typeof(string));

				Assert.Equal(parameters[index].Name, MessageTemplate);

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
