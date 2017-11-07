using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using Serilog.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Sdk;
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable AssignNullToNotNullAttribute

namespace Serilog.Tests
{
    /// <summary>
    /// The goal of these tests is to test API conformance,
    /// against classes that implement the ILogger interface
    /// </summary>
    [Collection("Log.Logger")]
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

        [Theory]
        [InlineData(Write)]
        [InlineData(nameof(LogEventLevel.Verbose))]
        [InlineData(nameof(LogEventLevel.Debug))]
        [InlineData(nameof(LogEventLevel.Information))]
        [InlineData(nameof(LogEventLevel.Warning))]
        [InlineData(nameof(LogEventLevel.Error))]
        [InlineData(nameof(LogEventLevel.Fatal))]
        public void SilentLoggerValidateConventions(string setName)
        {
            ValidateConventionForMethodSet(setName, typeof(SilentLogger),
                checkMesgTempAttr: false, testInvokeResults: false);
        }

        [Theory]
        [InlineData(typeof(SilentLogger))]
        [InlineData(typeof(Logger))]
        [InlineData(typeof(Log))]
        [InlineData(typeof(ILogger))]
        public void ValidateWriteEventLogMethods(Type loggerType)
        {
            var methods = loggerType.GetMethods()
                .Where(method => method.Name == Write && method.GetParameters()
                .Any(param => param.ParameterType == typeof(LogEvent)));

            Assert.Single(methods);

            var writeMethod = methods.Single();

            Assert.True(writeMethod.IsPublic);
            Assert.Equal(writeMethod.ReturnType, typeof(void));

            var level = LogEventLevel.Information;

            CollectingSink sink;
            var logger = GetLogger(loggerType, out sink);

            InvokeMethod(writeMethod, logger, new object[] { Some.LogEvent(DateTimeOffset.Now, level) });

            //handle silent logger special case i.e. no result validation
            if (loggerType == typeof(SilentLogger))
                return;

            EvaluateSingleResult(level, sink);
        }

        [Theory]
        [InlineData(typeof(SilentLogger))]
        [InlineData(typeof(Logger))]
        [InlineData(typeof(Log))]
        [InlineData(typeof(ILogger))]
        public void ValidateForContextMethods(Type loggerType)
        {
            var methodSet = loggerType.GetMethods().Where(method => method.Name == "ForContext");

            var testMethods = typeof(MethodOverloadConventionTests).GetRuntimeMethods()
                    .Where(method => Regex.IsMatch(method.Name, "ForContextMethod\\d"));

            Assert.Equal(testMethods.Count(), methodSet.Count());

            foreach (var method in methodSet)
            {
                Assert.Equal(method.ReturnType, typeof(ILogger));
                Assert.True(method.IsPublic);

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
                    }
                }

                Assert.True(signatureMatchAndInvokeSuccess, $"{method} did not match any known method or failed invoke\n" + report);
            }
        }

        [Theory]
        [InlineData(typeof(SilentLogger))]
        [InlineData(typeof(Logger))]
        [InlineData(typeof(Log))]
        [InlineData(typeof(ILogger))]
        public void ValidateBindMessageTemplateMethods(Type loggerType)
        {
            var method = loggerType.GetMethod("BindMessageTemplate");

            Assert.NotNull(method);
            Assert.Equal(method.ReturnType, typeof(bool));
            Assert.True(method.IsPublic);

            var messageTemplateAttr = method.GetCustomAttribute<MessageTemplateFormatMethodAttribute>();

            Assert.NotNull(messageTemplateAttr);
            Assert.Equal(messageTemplateAttr.MessageTemplateParameterName, MessageTemplate);

            var parameters = method.GetParameters();
            var index = 0;

            Assert.Equal(parameters[index].Name, "messageTemplate");
            Assert.Equal(parameters[index].ParameterType, typeof(string));
            index++;

            Assert.Equal(parameters[index].Name, "propertyValues");
            Assert.Equal(parameters[index].ParameterType, typeof(object[]));
            index++;

            Assert.Equal(parameters[index].Name, "parsedTemplate");
            Assert.Equal(parameters[index].ParameterType, typeof(MessageTemplate).MakeByRefType());
            Assert.True(parameters[index].IsOut);
            index++;

            Assert.Equal(parameters[index].Name, "boundProperties");
            Assert.Equal(parameters[index].ParameterType, typeof(IEnumerable<LogEventProperty>).MakeByRefType());
            Assert.True(parameters[index].IsOut);

            var logger = GetLogger(loggerType);

            var args = new object[]
            {
                "Processed {value0}, {value1}", new object[] { "value0", "value1" }, null, null
            };

            var result = InvokeMethod(method, logger, args);

            Assert.IsType(typeof(bool), result);

            //silentlogger is always false
            if (loggerType == typeof(SilentLogger))
                return;

            Assert.True(result as bool?);

            //test null arg path
            var falseResult = InvokeMethod(method, logger, new object[] { null, null, null, null });

            Assert.IsType(typeof(bool), falseResult);
            Assert.False(falseResult as bool?);
        }

        [Theory]
        [InlineData(typeof(SilentLogger))]
        [InlineData(typeof(Logger))]
        [InlineData(typeof(Log))]
        [InlineData(typeof(ILogger))]
        public void ValidateBindPropertyMethods(Type loggerType)
        {
            var method = loggerType.GetMethod("BindProperty");

            Assert.NotNull(method);
            Assert.Equal(method.ReturnType, typeof(bool));
            Assert.True(method.IsPublic);

            var parameters = method.GetParameters();
            var index = 0;

            Assert.Equal(parameters[index].Name, "propertyName");
            Assert.Equal(parameters[index].ParameterType, typeof(string));
            index++;

            Assert.Equal(parameters[index].Name, "value");
            Assert.Equal(parameters[index].ParameterType, typeof(object));
            index++;

            Assert.Equal(parameters[index].Name, "destructureObjects");
            Assert.Equal(parameters[index].ParameterType, typeof(bool));
            index++;

            Assert.Equal(parameters[index].Name, "property");
            Assert.Equal(parameters[index].ParameterType, typeof(LogEventProperty).MakeByRefType());
            Assert.True(parameters[index].IsOut);

            var logger = GetLogger(loggerType);

            var args = new object[]
            {
                "SomeString", "someString", false, null
            };

            var result = InvokeMethod(method, logger, args);

            Assert.IsType(typeof(bool), result);

            //silentlogger will always be false
            if (loggerType == typeof(SilentLogger))
                return;

            Assert.True(result as bool?);

            //test null arg path/ invalid property name
            var falseResult = InvokeMethod(method, logger, new object[] { " ", null, false, null });

            Assert.IsType(typeof(bool), falseResult);
            Assert.False(falseResult as bool?);
        }

        [Theory]
        [InlineData(typeof(SilentLogger))]
        [InlineData(typeof(Logger))]
        [InlineData(typeof(Log))]
        [InlineData(typeof(ILogger))]
        public void ValidateIsEnabledMethods(Type loggerType)
        {
            var method = loggerType.GetMethod("IsEnabled");

            Assert.NotNull(method);
            Assert.True(method.IsPublic);
            Assert.Equal(method.ReturnType, typeof(bool));

            var parameters = method.GetParameters();

            Assert.Single(parameters);

            var parameter = parameters.Single();

            Assert.Equal(parameter.Name, "level");
            Assert.Equal(parameter.ParameterType, typeof(LogEventLevel));

            var logger = GetLogger(loggerType, out _, LogEventLevel.Information);

            var falseResult = InvokeMethod(method, logger, new object[] { LogEventLevel.Verbose });

            Assert.IsType(typeof(bool), falseResult);
            Assert.False(falseResult as bool?);

            var trueResult = InvokeMethod(method, logger, new object[] { LogEventLevel.Warning });

            Assert.IsType(typeof(bool), trueResult);

            //return as silentlogger will always be false
            if (loggerType == typeof(SilentLogger))
                return;

            Assert.True(trueResult as bool?);
        }

        //public ILogger ForContext(ILogEventEnricher enricher)
        void ForContextMethod0(MethodInfo method)
        {
            try
            {
                var parameters = method.GetParameters();

                Assert.Single(parameters);

                var parameter = parameters.Single();

                Assert.Equal(parameter.Name, "enricher");
                Assert.Equal(parameter.ParameterType, typeof(ILogEventEnricher));
            }
            catch (XunitException e)
            {
                e.Data.Add("IsSignatureAssertionFailure", true);

                throw;
            }

            var logger = GetLogger(method.DeclaringType);

            var logEnricher = new TestDummies.DummyThreadIdEnricher();

            var enrichedLogger = InvokeMethod(method, logger, new object[] { logEnricher });

            TestForContextResult(method, logger, normalResult: enrichedLogger);
        }

        //public ILogger ForContext(ILogEventEnricher[] enricher)
        void ForContextMethod1(MethodInfo method)
        {
            try
            {
                var parameters = method.GetParameters();

                Assert.Single(parameters);

                var parameter = parameters.Single();

                Assert.Equal(parameter.Name, "enrichers");

                Assert.True(parameter.ParameterType == typeof(IEnumerable<ILogEventEnricher>)
                    || parameter.ParameterType == typeof(ILogEventEnricher[]));
            }
            catch (XunitException e)
            {
                e.Data.Add("IsSignatureAssertionFailure", true);

                throw;
            }

            var logger = GetLogger(method.DeclaringType);

            var logEnricher = new TestDummies.DummyThreadIdEnricher();

            var enrichedLogger = InvokeMethod(method, logger,
                new object[] { new ILogEventEnricher[] { logEnricher, logEnricher } });

            TestForContextResult(method, logger, normalResult: enrichedLogger);
        }

        //public ILogger ForContext(string propertyName, object value, bool destructureObjects)
        void ForContextMethod2(MethodInfo method)
        {
            try
            {
                var parameters = method.GetParameters();
                Assert.Equal(parameters.Length, 3);

                var index = 0;

                Assert.Equal(parameters[index].Name, "propertyName");
                Assert.Equal(parameters[index].ParameterType, typeof(string));
                index++;

                Assert.Equal(parameters[index].Name, "value");
                Assert.Equal(parameters[index].ParameterType, typeof(object));
                index++;

                Assert.Equal(parameters[index].Name, "destructureObjects");
                Assert.Equal(parameters[index].ParameterType, typeof(bool));
                Assert.True(parameters[index].IsOptional);
            }
            catch (XunitException e)
            {
                e.Data.Add("IsSignatureAssertionFailure", true);

                throw;
            }

            var logger = GetLogger(method.DeclaringType);

            var propertyName = "SomeString";
            var propertyValue = "someString";

            var enrichedLogger = InvokeMethod(method, logger, new object[] { propertyName, propertyValue, false });

            Assert.NotNull(enrichedLogger);
            Assert.True(enrichedLogger is ILogger);

            //silentlogger will always return itself
            if (method.DeclaringType == typeof(SilentLogger))
                return;

            Assert.NotSame(logger, enrichedLogger);

            //invalid args path
            var sameLogger = InvokeMethod(method, logger, new object[] { null, null, false });

            Assert.NotNull(sameLogger);
            Assert.True(sameLogger is ILogger);

            if (method.DeclaringType == typeof(Log))
                Assert.Same(Log.Logger, sameLogger);
            else
                Assert.Same(logger, sameLogger);
        }

        //public ILogger ForContext<TSource>()
        void ForContextMethod3(MethodInfo method)
        {
            try
            {
                Assert.True(method.IsGenericMethod);

                var genericArgs = method.GetGenericArguments();

                Assert.Single(genericArgs);

                var genericArg = genericArgs.Single();

                Assert.Equal(genericArg.Name, "TSource");
            }
            catch (XunitException e)
            {
                e.Data.Add("IsSignatureAssertionFailure", true);

                throw;
            }

            var logger = GetLogger(method.DeclaringType);

            var enrichedLogger = InvokeMethod(method, logger, null, new[] { typeof(object) });

            Assert.NotNull(enrichedLogger);
            Assert.True(enrichedLogger is ILogger);
        }

        //public ILogger ForContext(Type source)
        void ForContextMethod4(MethodInfo method)
        {
            try
            {
                var args = method.GetParameters();

                Assert.Single(args);

                var arg = args.Single();

                Assert.Equal(arg.Name, "source");
                Assert.Equal(arg.ParameterType, typeof(Type));
            }
            catch (XunitException e)
            {
                e.Data.Add("IsSignatureAssertionFailure", true);

                throw;
            }

            var logger = GetLogger(method.DeclaringType);

            var enrichedLogger = InvokeMethod(method, logger, new object[] { typeof(object) });

            TestForContextResult(method, logger, normalResult: enrichedLogger);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        void TestForContextResult(MethodInfo method, ILogger logger, object normalResult)
        {
            Assert.NotNull(normalResult);
            Assert.True(normalResult is ILogger);

            if (method.DeclaringType == typeof(SilentLogger))
                return;

            Assert.NotSame(logger, normalResult);

            //if invoked with null args it should return the same instance
            var sameLogger = InvokeMethod(method, logger, new object[] { null });

            Assert.NotNull(sameLogger);

            if (method.DeclaringType == typeof(Log))
                Assert.Same(Log.Logger, sameLogger);
            else
                Assert.Same(logger, sameLogger);
        }

        void ValidateConventionForMethodSet(
            string setName,
            Type loggerType,
            bool checkMesgTempAttr = true,
            bool testInvokeResults = true)
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

                if (checkMesgTempAttr)
                {
                    var messageTemplateAttr = method.GetCustomAttribute<MessageTemplateFormatMethodAttribute>();

                    Assert.NotNull(messageTemplateAttr);
                    Assert.Equal(messageTemplateAttr.MessageTemplateParameterName, MessageTemplate);
                }

                var signatureMatchAndInvokeSuccess = false;

                var report = new StringBuilder();

                foreach (var testMethod in testMethods)
                {
                    try
                    {
                        Action<MethodInfo, Type[], object[]> invokeTestMethod;

                        if (testInvokeResults)
                            invokeTestMethod = InvokeConventionMethodAndTest;
                        else
                            invokeTestMethod = InvokeConventionMethod;

                        testMethod.Invoke(this, new object[] { method, invokeTestMethod });

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
                    }
                }

                Assert.True(signatureMatchAndInvokeSuccess, $"{method} did not match any known convention or failed invoke\n" + report);
            }
        }

        // Method0 (string messageTemplate) : void
        void ValidateMethod0(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method);

            var parameters = new object[] { "message" };

            invokeMethod(method, null, parameters);
        }

        // Method1<T> (string messageTemplate, T propertyValue) : void
        void ValidateMethod1(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 2);

            var typeArgs = new[] { typeof(string) };

            var parameters = new object[] { "message", "value0" };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method2<T0, T1> (string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
        void ValidateMethod2(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 3);

            var typeArgs = new[] { typeof(string), typeof(string) };

            var parameters = new object[]
            {
                "Processed {value0}, {value1}", "value0", "value1"
            };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method3<T0, T1, T2> (string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
        void ValidateMethod3(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, isGeneric: true, expectedArgCount: 4);

            var typeArgs = new[] { typeof(string), typeof(string), typeof(string) };

            var parameters = new object[]
            {
                "Processed {value0}, {value1}, {value2}", "value0", "value1", "value2"
            };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method4 (string messageTemplate, params object[] propertyValues) : void
        void ValidateMethod4(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, expectedArgCount: 2);

            var parameters = new object[]
            {
                "Processed {value0}, {value1}, {value2}", new object[] { "value0", "value1", "value2" }
            };

            invokeMethod(method, null, parameters);
        }

        // Method5 (Exception exception, string messageTemplate) : void
        void ValidateMethod5(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 2);

            var parameters = new object[] { new Exception("test"), "message" };

            invokeMethod(method, null, parameters);
        }

        // Method6<T> (Exception exception, string messageTemplate, T propertyValue) : void
        void ValidateMethod6(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 3);

            var typeArgs = new[] { typeof(string) };

            var parameters = new object[]
            {
                new Exception("test"), "Processed {value0}", "value0"
            };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method7<T0, T1> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) : void
        void ValidateMethod7(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 4);

            var typeArgs = new[] { typeof(string), typeof(string) };

            var parameters = new object[]
            {
                new Exception("test"), "Processed {value0}, {value1}", "value0", "value1"
            };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method8<T0, T1, T2> (Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) : void
        void ValidateMethod8(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, hasExceptionArg: true, isGeneric: true, expectedArgCount: 5);

            var typeArgs = new[] { typeof(string), typeof(string), typeof(string) };

            var parameters = new object[]
            {
                new Exception("test"), "Processed {value0}, {value1}, {value2}", "value0", "value1", "value2"
            };

            invokeMethod(method, typeArgs, parameters);
        }

        // Method9 (Exception exception, string messageTemplate, params object[] propertyValues) : void
        void ValidateMethod9(MethodInfo method, Action<MethodInfo, Type[], object[]> invokeMethod)
        {
            VerifyMethodSignature(method, hasExceptionArg: true, expectedArgCount: 3);

            var parameters = new object[]
            {
                new Exception("test"), "Processed {value0}, {value1}, {value2}",
                new object[] { "value0", "value1", "value2" }
            };

            invokeMethod(method, null, parameters);
        }

        //primarily meant for testing silent logger
        static void InvokeConventionMethod(
            MethodInfo method,
            Type[] typeArgs,
            object[] parameters,
            out LogEventLevel level,
            out CollectingSink sink)
        {
            var logger = GetLogger(method.DeclaringType, out sink);

            if (method.Name == Write)
            {
                level = LogEventLevel.Information;

                var paramList = new List<object>() { level };

                paramList.AddRange(parameters);

                parameters = paramList.ToArray();
            }
            else
                Assert.True(Enum.TryParse(method.Name, out level));

            InvokeMethod(method, logger, parameters, typeArgs);
        }

        static void InvokeConventionMethod(MethodInfo method, Type[] typeArgs, object[] parameters)
        {
            InvokeConventionMethod(method, typeArgs, parameters, out _, out _);
        }

        static void InvokeConventionMethodAndTest(MethodInfo method, Type[] typeArgs, object[] parameters)
        {
            CollectingSink sink;
            LogEventLevel level;

            InvokeConventionMethod(method, typeArgs, parameters, out level, out sink);

            EvaluateSingleResult(level, sink);
        }

        // parameters will always be ordered so single evaluation method will work
        static void VerifyMethodSignature(MethodInfo method, bool hasExceptionArg = false, bool isGeneric = false, int expectedArgCount = 1)
        {
            try
            {
                var parameters = method.GetParameters();

                var index = 0;

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
                        for (var i = 0; i < genericTypeArgs.Length; i++, index++)
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

                throw;
            }
        }

        static object InvokeMethod(
            MethodInfo method,
            ILogger instance,
            object[] parameters,
            Type[] typeArgs = null)
        {
            if (method.IsStatic)
            {
                if (method.IsGenericMethod)
                    return method.MakeGenericMethod(typeArgs).Invoke(null, parameters);

                return method.Invoke(null, parameters);
            }

            if (method.IsGenericMethod)
                return method.MakeGenericMethod(typeArgs).Invoke(instance, parameters);

            return method.Invoke(instance, parameters);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        static void EvaluateSingleResult(LogEventLevel level, CollectingSink results)
        {
            //evaluate single log event
            Assert.Equal(1, results.Events.Count);

            var evt = results.Events.Single();

            Assert.Equal(level, evt.Level);
        }

        static ILogger GetLogger(Type loggerType)
        {
            return GetLogger(loggerType, out _);
        }

        static ILogger GetLogger(Type loggerType, out CollectingSink sink, LogEventLevel level = LogEventLevel.Verbose)
        {
            sink = null;

            if (loggerType == typeof(Logger) || loggerType == typeof(ILogger))
            {
                sink = new CollectingSink();

                return new LoggerConfiguration()
                    .MinimumLevel.Is(level)
                    .WriteTo.Sink(sink)
                    .CreateLogger();
            }

            if (loggerType == typeof(Log))
            {
                sink = new CollectingSink();

                Log.CloseAndFlush();

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(level)
                    .WriteTo.Sink(sink)
                    .CreateLogger();

                return null;
            }

            if (loggerType == typeof(SilentLogger))
                return new SilentLogger();

            throw new ArgumentException($"Logger Type of {loggerType} is not supported");
        }
    }
}
