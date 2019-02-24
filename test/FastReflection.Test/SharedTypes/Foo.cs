using System;
using System.Collections.Generic;
using System.Text;

namespace FastReflection.Test.SharedTypes
{
    public class Foo
    {
        public static string[] StaticEcho(string arg1, string arg2) =>
              new[] { arg1, arg2 };

        public static int[] StaticEcho(int arg1, int arg2) =>
            new[] { arg1, arg2 };

        public int[] InstanceEcho(int arg1, int arg2) => new[] { arg1, arg2 };
    }
}
