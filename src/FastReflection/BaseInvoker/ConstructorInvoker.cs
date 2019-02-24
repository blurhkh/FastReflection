using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace FastReflection.BaseInvoker
{
    public class ConstructorInvoker : IConstructorInvoker
    {
        private readonly Func<object[], object> _invoker;

        public ConstructorInfo ConstructorInfo { get; }

        public ConstructorInvoker(ConstructorInfo info)
        {
            ConstructorInfo = info;
            _invoker = CreateInvokeDelegate(info);
        }

        public object Invoke(params object[] parameters)
        {
            return _invoker(parameters);
        }

        public static Func<object[], object> CreateInvokeDelegate(ConstructorInfo info)
        {
            var dm = new DynamicMethod(Guid.NewGuid().ToString(), typeof(object), new[] { typeof(object[]) });

            var il = dm.GetILGenerator();

            int index = 0;

            foreach (var param in info.GetParameters())
            {
                il.Emit(OpCodes.Ldarg_0);
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

            il.Emit(OpCodes.Newobj, info);
            il.Emit(OpCodes.Ret);

            return (Func<object[], object>)dm.CreateDelegate(typeof(Func<object[], object>));
        }
    }
}
