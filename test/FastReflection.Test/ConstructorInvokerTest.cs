using System;
using FastReflection.BaseInvoker;
using FastReflection.Test.SharedTypes;
using Xunit;

namespace FastReflection.Test
{
    public class ConstructorInvokerTest
    {
        [Fact]
        public  void TestConstructorWithArgs()
        {
            // Arrange
            var constructorInfo = typeof(Bar).GetConstructor(new[] {typeof(decimal), typeof(string)});
            decimal arg1 = 1.11m;
            string arg2 = "123";

            // Act
            var invoker = new ConstructorInvoker(constructorInfo);
            var obj = invoker.Invoke(arg1, arg2) as Bar;

            // Assert
            Assert.Equal(arg1, obj.DecimalProp);
            Assert.Equal(arg2, obj.StringProp);
        }
    }
}