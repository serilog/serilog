using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Tests.Support;

namespace Serilog.Extras.DestructureByIgnoring.Tests
{
    [TestFixture]
    public class DestructureByIgnoringTests
    {
        class DestructureMe
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }

        [Test]
        public void PropertyNamesInExpressionsAreIgnoredWhenDestructuring()
        {
            LogEvent evt = null;

            Expression<Func<DestructureMe, object>> valueTypeProperty = dm => dm.Id;
            Expression<Func<DestructureMe, object>> referenceTypeProperty = dm => dm.Password;

            var log = new LoggerConfiguration()
                .Destructure.ByIgnoringProperties(valueTypeProperty, referenceTypeProperty)
                .WriteTo.Sink(new DelegatingSink(e => evt = e))
                .CreateLogger();
            
            var ignored = new DestructureMe
            {
                Id = 2,
                Name = "Name",
                Password = "Password"
            };

            log.Information("Here is {@Ignored}", ignored);

            var sv = (StructureValue)evt.Properties["Ignored"];
            var props = sv.Properties.ToDictionary(p => p.Name, p => p.Value);

            Assert.IsFalse(props.ContainsKey("Id"), "Id property should have been ignored");
            Assert.IsFalse(props.ContainsKey("Password"), "Password property should have been ignored.");
            Assert.AreEqual("Name", props["Name"].LiteralValue());
        }

        [Test]
        public void ComplexExpressionsFail()
        {
            AssertUnsupportedExpression<DestructureMe>(dm => new
            {
                Name = dm.Name
            });
        }

        [Test]
        public void MethodExpressionsFail()
        {
            AssertUnsupportedExpression<DestructureMe>(dm => dm.ToString());
        }

        [Test]
        public void StringLiteralExpressionsFail()
        {
            AssertUnsupportedExpression<DestructureMe>(dm => "string literal");
        }

        [Test]
        public void ChainedPropertyExpressionsFail()
        {
            AssertUnsupportedExpression<DestructureMe>(dm => dm.Password.Length);
        }

        private void AssertUnsupportedExpression<T>(Expression<Func<T, object>> expressionThatShouldFail)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                    new LoggerConfiguration()
                    .Destructure
                    .ByIgnoringProperties(expressionThatShouldFail)
            );

            Assert.That(ex.ParamName, Is.EqualTo("ignoredProperty"));
        }
    }
}
