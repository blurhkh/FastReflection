
using System;
using System.Collections.Generic;
using System.Text;

namespace FastReflection.BaseInvoker
{
    public interface IConstructorInvoker
    {
        object Invoke(params object[] parameters);
    }
}
