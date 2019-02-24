using System;
using System.Collections.Generic;
using System.Text;
using FastReflection.BaseAccessor;
using FastReflection.BaseInvoker;
using FastReflection.Test.SharedTypes;
using Xunit;

namespace FastReflection.Test
{
    public class PropertyAccessorTest
    {
        [Fact]
        public void TestPropertyGet()
        {
            // Arrange
            decimal arg1 = 1.11m;
            string arg2 = "123";
            var bar = new Bar(arg1, arg2);
            var propInfo1 = typeof(Bar).GetProperty(nameof(Bar.DecimalProp));
            var propInfo2 = typeof(Bar).GetProperty(nameof(Bar.StringProp));

            // Act
            var res1 = new PropertyAccessor(propInfo1).GetValue(bar);
            var res2 = new PropertyAccessor(propInfo2).GetValue(bar);

            // Assert
            Assert.Equal(arg1,res1);
            Assert.Equal(arg2, res2);
        }
    }
}
