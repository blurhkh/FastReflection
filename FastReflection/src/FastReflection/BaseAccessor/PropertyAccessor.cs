using System;
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


            il.Emit(OpCodes.Ret);

            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }

        public static Action<object, object> CreateSetPropertyDelegate(PropertyInfo info)
        {
            if (info.CanWrite == false) return null;

            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(void), new[] { typeof(object), typeof(object) });

            var il = dm.GetILGenerator();

            var setMethodInfo = info.SetMethod;

            if (setMethodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Call, setMethodInfo);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Callvirt, setMethodInfo);
            }
            il.Emit(OpCodes.Ret);

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }
    }
}