using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FastReflection.BaseInvoker
{
    public class MethodInvoker : IMethodInvoker
    {
        private readonly Func<object, object[], object> _invoker;

        public MethodInfo MethodInfo { get; }

        public MethodInvoker(MethodInfo info)
        {
            MethodInfo = info;
            _invoker = CreateInvokerDelegate(info);
        }

        public object Invoke(object instance, params object[] parameters)
        {
            return _invoker(instance, parameters);
        }

        public static Func<object, object[], object> CreateInvokerDelegate(MethodInfo info)
        {
            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(object), new[] { typeof(object), typeof(object[]) });

            var il = dm.GetILGenerator();

            if (info.IsStatic == false) il.Emit(OpCodes.Ldarg_0);

            int index = 0;

            foreach (var param in info.GetParameters())
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldc_I4, index++);

                var paramType = param.ParameterType;
                il.Emit(OpCodes.Ldelem_Ref);

                if (paramType.IsValueType)
                {
                    // 值类型的话进行拆箱
                    il.Emit(OpCodes.Unbox_Any, paramType);
                }
                else if (paramType != typeof(object))
                {
                    // 非 Object 的引用类型进行类型转换
                    il.Emit(OpCodes.Castclass, paramType);
                }
            }

            il.Emit(info.IsStatic ? OpCodes.Call : OpCodes.Callvirt, info);

            if (info.ReturnType == typeof(void))
            {
                // 如果没有返回值，返回 null
                il.Emit(OpCodes.Ldnull);
            }

            il.Emit(OpCodes.Ret);

            return (Func<object, object[], object>)dm.CreateDelegate(typeof(Func<object, object[], object>));
        }
    }
}
