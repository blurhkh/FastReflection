using System;
using System.Collections.Generic;
using System.Text;

namespace FastReflection.BaseAccessor
{
    public interface IFieldAccessor
    {
        object GetValue(object instance);

        void SetValue(object instance, object value);
    }
}
