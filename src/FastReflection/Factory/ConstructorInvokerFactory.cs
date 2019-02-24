using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FastReflection.BaseInvoker;
using FastReflection.Caches;

namespace FastReflection.Factory
{
    public class ConstructorInvokerFactory : IFastReflectionFactory<ConstructorInfo, IConstructorInvoker>
    {
        public IConstructorInvoker Create(ConstructorInfo key) => new ConstructorInvoker(key);
    }
}
