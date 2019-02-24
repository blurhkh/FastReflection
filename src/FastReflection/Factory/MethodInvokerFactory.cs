
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FastReflection.BaseInvoker;

namespace FastReflection.Factory
{
    public class MethodInvokerFactory : IFastReflectionFactory<MethodInfo, IMethodInvoker>
    {
        public IMethodInvoker Create(MethodInfo key) => new MethodInvoker(key);
        
    }
}
