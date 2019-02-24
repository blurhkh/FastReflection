using System;
using System.Linq;
using System.Reflection;

namespace FastReflection.Caches
{
    public class TypeInfoCache
    {
        private ConstructorInfo[] _constructorInfos;
        private FieldInfo[] _fieldInfos;
        private PropertyInfo[] _propertyInfos;
        private MethodInfo[] _methodInfos;
        private Attribute[] _attributes;
        private CustomAttributeData[] _customAttributeData;

        public ConstructorInfo[] ConstructorInfos =>
            _constructorInfos ?? (_constructorInfos = Type.GetConstructors());

        public FieldInfo[] FieldInfos =>
            _fieldInfos ?? (_fieldInfos = Type.GetFields());

        public PropertyInfo[] PropertyInfos =>
            _propertyInfos ?? (_propertyInfos = Type.GetProperties());

        public MethodInfo[] MethodInfos =>
            _methodInfos ?? (_methodInfos = Type.GetMethods());

        public Attribute[] Attributes =>
            _attributes ?? (_attributes = Type.GetCustomAttributes().ToArray());

        public CustomAttributeData[] CustomAttributeData =>
            _customAttributeData ?? (_customAttributeData = Type.GetCustomAttributesData().ToArray());

        public Type Type { get; }

        public TypeInfoCache(Type type)
        {
            Type = type;
        }
    }
}
