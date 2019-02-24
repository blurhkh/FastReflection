
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FastReflection.BaseAccessor;

namespace FastReflection.Factory
{
    class PropertyAccessorFactory : IFastReflectionFactory<PropertyInfo, IPropertyAccessor>
    {
        public IPropertyAccessor Create(PropertyInfo key) => new PropertyAccessor(key);
    }
}
