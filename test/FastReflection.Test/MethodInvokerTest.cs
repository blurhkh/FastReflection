using System;
using System.Collections;
using System.Collections.Generic;
using FastReflection.BaseInvoker;
using FastReflection.Test.SharedTypes;
using Xunit;

namespace FastReflection.Test
{
    public class MethodInvokerTest
    {
        [Fact]
        public void TestStaticMethodWithArgs()
        {
            // Arrange
            var methodInfo1= new Func<string,string,string[]>(Foo.StaticEcho).Method;
            var methodInfo2 = new Func<int, int, int[]>(Foo.StaticEcho).Method;
            var arg1 = "1";
            var arg2 = "2";

            int arg3 = 1;
            int arg4 = 2;

            // Act
            var invoker1 = new MethodInvoker(methodInfo1);
            var invoker2 = new MethodInvoker(methodInfo2);
            var res1 = invoker1.Invoke(null, arg1, arg2);
            var res2 = invoker2.Invoke(null, arg3, arg4);


            // Assert
            Assert.Equal(new[] { arg1, arg2 }, (IEnumerable<string>)res1);
            Assert.Equal(new[] { arg3, arg4 }, (IEnumerable<int>)res2);
        }

        [Fact]
        public void TestStaticMethodWithoutArgs()
        {

        }

        [Fact]
        public void TestStaticMethodWithoutReturnType()
        {

        }

        [Fact]
        public void TestInstanceMethodWithArgs()
        {
            // Arrange
            var foo = new Foo();
            var methodInfo = new Func<int, int, int[]>(foo.InstanceEcho).Method;

            int arg1 = 1;
            int arg2 = 2;

            // Act
            var invoker = new MethodInvoker(methodInfo);
            var res = invoker.Invoke(foo, arg1, arg2);


            // Assert
            Assert.Equal(new[] { arg1, arg2 }, (IEnumerable<int>)res);
        }

        [Fact]
        public void TestInstanceMethodWithoutArgs()
        {

        }
    }
}
