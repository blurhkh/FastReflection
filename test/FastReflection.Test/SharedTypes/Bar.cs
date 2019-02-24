using System;
using System.Collections.Generic;
using System.Text;

namespace FastReflection.Test.SharedTypes
{
    public class Bar
    {
        public decimal DecimalProp { get; set; }

        public string StringProp { get; set; }

        public Bar(decimal decimalProp, string stringProp)
        {
            DecimalProp = decimalProp;
            StringProp = stringProp;
        }
    }
}
