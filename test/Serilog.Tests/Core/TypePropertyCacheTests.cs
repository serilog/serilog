namespace Serilog.Tests.Core
{
    using NUnit.Framework;
    using Serilog.Parameters;

    [TestFixture]
    class TypePropertyCacheTests
    {
     
        [Test]
        public void CanReadFromClassWithClassProperty()
        {
            var instance = new ClassWithClassProperty
            {
                ClassProperty = "10"
            };
            var expression = TypePropertyCache.GetGetMethodByExpression(instance.GetType().GetProperty("ClassProperty"));
            Assert.AreEqual("10", expression(instance));
        }
        public class ClassWithClassProperty
        {
            public string ClassProperty { get; set; }
        }
        [Test]
        public void CanReadFromClassWithStructProperty()
        {
            var instance = new ClassWithStructProperty
            {
                StructProperty = 10
            };
            var expression = TypePropertyCache.GetGetMethodByExpression(instance.GetType().GetProperty("StructProperty"));
            Assert.AreEqual(10, expression(instance));
        }
        public class ClassWithStructProperty
        {
            public int StructProperty { get; set; }
        }
        [Test]
        public void CanReadFromStructWithClassProperty()
        {
            var instance = new StructWithClassProperty
            {
                ClassProperty = "10"
            };
            var expression = TypePropertyCache.GetGetMethodByExpression(instance.GetType().GetProperty("ClassProperty"));
            Assert.AreEqual("10", expression(instance));
        }
        public class StructWithClassProperty
        {
            public string ClassProperty { get; set; }
        }
        [Test]
        public void CanReadFromStructWithStructProperty()
        {
            var instance = new StructWithStructProperty
            {
                StructProperty = 10
            };
            var expression = TypePropertyCache.GetGetMethodByExpression(instance.GetType().GetProperty("StructProperty"));
            Assert.AreEqual(10, expression(instance));
        }
        public class StructWithStructProperty
        {
            public int StructProperty { get; set; }
        }
    }
}
