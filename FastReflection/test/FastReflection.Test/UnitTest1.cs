using System;
using System.Reflection;
using FastReflection.BaseAccessor;
using Xunit;

namespace FastReflection.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var fieldInfo = typeof(Test).GetField(nameof(Test.Foo), BindingFlags.Static | BindingFlags.Public);

            new FieldAccessor(fieldInfo).SetValue(null, "2");

            Assert.True((string)new FieldAccessor(fieldInfo).GetValue(null) == "2");
        }

        [Fact]
        public void Test2()
        {
            PropertyInfo propertyInfo = typeof(Foo).GetProperty(nameof(Foo.Bar));

            var foo = new Foo();

            new PropertyAccessor(propertyInfo).SetValue(foo, "123");

            Assert.True((string)new PropertyAccessor(propertyInfo).GetValue(foo) == "123");
        }
    }
}
