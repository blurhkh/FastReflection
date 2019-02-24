using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace FastReflection.BaseAccessor
{
    public class PropertyAccessor : IPropertyAccessor
    {
        private readonly Func<object, object> _getter;
        private readonly Action<object, object> _setter;

        public PropertyInfo PropertyInfo { get; }

        public PropertyAccessor(PropertyInfo info)
        {
            PropertyInfo = info;
            _getter = CreateGetPropertyDelegate(info);
            _setter = CreateSetPropertyDelegate(info);
        }

        public object GetValue(object instance)
        {
            return _getter(instance);
        }

        public void SetValue(object instance, object value)
        {
            _setter(instance, value);
        }

        public static Func<object, object> CreateGetPropertyDelegate(PropertyInfo info)
        {
            if (info.CanRead == false) return null;
            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(object), new[] { typeof(object) });

            var il = dm.GetILGenerator();

            var getMethodInfo = info.GetMethod;

            if (getMethodInfo.IsStatic)
            {
                il.Emit(OpCodes.Call, getMethodInfo);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, getMethodInfo);
            }

            // 如果返回值是值类型，需要装箱
            if (getMethodInfo.ReturnType.IsValueType)
            {
                il.Emit(OpCodes.Box, getMethodInfo.ReturnType);
            }

            il.Emit(OpCodes.Ret);

            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }

        public static Action<object, object> CreateSetPropertyDelegate(PropertyInfo info)
        {
            if (info.CanWrite == false) return null;

            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(void), new[] { typeof(object), typeof(object) });

            var il = dm.GetILGenerator();

            var setMethodInfo = info.SetMethod;

            if (setMethodInfo.IsStatic == false) il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldarg_1);

            // 如果参数是值类型，需要拆箱
            var paramType = setMethodInfo.GetParameters().Single().ParameterType;
            if (paramType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, paramType);
            }

            il.Emit(setMethodInfo.IsStatic ? OpCodes.Call : OpCodes.Callvirt, setMethodInfo);

            il.Emit(OpCodes.Ret);

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }
    }
}