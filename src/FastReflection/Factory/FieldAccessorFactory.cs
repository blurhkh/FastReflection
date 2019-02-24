
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FastReflection.BaseAccessor;

namespace FastReflection.Factory
{
    public class FieldAccessorFactory : IFastReflectionFactory<FieldInfo, IFieldAccessor>
    {
        public IFieldAccessor Create(FieldInfo key) => new FieldAccessor(key);
    }
}
