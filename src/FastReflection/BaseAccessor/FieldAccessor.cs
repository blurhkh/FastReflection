using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FastReflection.BaseAccessor
{
    public class FieldAccessor : IFieldAccessor
    {
        private readonly Func<object, object> _getter;

        private readonly Action<object, object> _setter;

        public FieldInfo FieldInfo { get; }

        public FieldAccessor(FieldInfo info)
        {
            FieldInfo = info;
            _getter = CreateGetDelegate(info);
            _setter = CreateSetDelegate(info);
        }

        public object GetValue(object instance) => _getter(instance);

        public void SetValue(object instance, object value) => _setter(instance, value);

        public static Action<object, object> CreateSetDelegate(FieldInfo info)
        {
            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(void), new[] { typeof(object), typeof(object) });

            var il = dm.GetILGenerator();

            if (info.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_1);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, info);
            }

            // 如果是值类型，需要拆箱
            if (info.FieldType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, info.FieldType);
            }

            il.Emit(info.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, info);

            il.Emit(OpCodes.Ret);

            return (Action<object, object>)dm.CreateDelegate(typeof(Action<object, object>));
        }

        public static Func<object, object> CreateGetDelegate(FieldInfo info)
        {
            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(object), new[] { typeof(object) });

            var il = dm.GetILGenerator();

            if (info.IsStatic)
            {
                il.Emit(OpCodes.Ldsfld, info);
            }
            else
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, info);
            }

            // 如果是值类型，需要装箱
            if (info.FieldType.IsValueType)
            {
                il.Emit(OpCodes.Box, info.FieldType);
            }

            il.Emit(OpCodes.Ret);

            return (Func<object, object>)dm.CreateDelegate(typeof(Func<object, object>));
        }

    }
}
