using System;
using System.Reflection;
using static FastReflection.Caches.FastReflectionCaches;

namespace FastReflection
{
    public static class FastReflectionExtensions
    {
        #region Type

        public static ConstructorInfo[] FastGetConstructors(this Type type) =>
            TypesCache.Get(type).ConstructorInfos;

        public static PropertyInfo[] FastGetProperties(this Type type) =>
            TypesCache.Get(type).PropertyInfos;

        public static FieldInfo[] FastGetFields(this Type type) =>
            TypesCache.Get(type).FieldInfos;

        public static MethodInfo[] FastGetMethods(this Type type) =>
            TypesCache.Get(type).MethodInfos;

        public static Attribute[] FastGetCustomAttributes(this Type type) =>
            TypesCache.Get(type).Attributes;

        public static CustomAttributeData[] FastGetCustomAttributeData(this Type type) =>
            TypesCache.Get(type).CustomAttributeData;

        #endregion

        #region ConstructorInfo

        public static object FastInvoke(this ConstructorInfo constructorInfo, params object[] parameters) =>
            ConstructorInvokerCache.Get(constructorInfo).Invoke(parameters);

        #endregion

        #region MethodInfo

        public static object FastInvoke(this MethodInfo info, object instance, params object[] parameters) =>
            MethodInvokerCache.Get(info).Invoke(instance, parameters);

        public static T FastInvoke<T>(this MethodInfo info, object instance, params object[] parameters) =>
            (T)MethodInvokerCache.Get(info).Invoke(instance, parameters);

        #endregion

        #region PropertyInfo

        public static object FastGetValue(this PropertyInfo propertyInfo, object instance) =>
            PropertyAccessorCache.Get(propertyInfo).GetValue(instance);

        public static T FastGetValue<T>(this PropertyInfo propertyInfo, object instance) =>
            (T)PropertyAccessorCache.Get(propertyInfo).GetValue(instance);

        public static void FastSetValue(this PropertyInfo propertyInfo, object instance, object value) =>
            PropertyAccessorCache.Get(propertyInfo).SetValue(instance, value);

        #endregion

        #region FieldInfo

        public static object FastGetValue(this FieldInfo fieldInfo, object instance) =>
            FieldAccessorCache.Get(fieldInfo).GetValue(instance);

        public static T FastGetValue<T>(this FieldInfo fieldInfo, object instance) =>
            (T)FieldAccessorCache.Get(fieldInfo).GetValue(instance);

        public static void FastSetValue(this FieldInfo fieldInfo, object instance, object value) =>
            FieldAccessorCache.Get(fieldInfo).SetValue(instance, value);

        #endregion
    }
}
