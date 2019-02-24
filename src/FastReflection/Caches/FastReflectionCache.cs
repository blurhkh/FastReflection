using FastReflection.BaseAccessor;
using FastReflection.Factory;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using FastReflection.BaseInvoker;

namespace FastReflection.Caches
{
    public abstract class FastReflectionCache<TKey, TValue> : IFastReflectionCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>(16);
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        public TValue Get(TKey key)
        {
            var value = default(TValue);

            _rwLock.EnterReadLock();
            bool cacheHit = _cache.TryGetValue(key, out value);
            _rwLock.ExitReadLock();

            if (cacheHit) return value;

            _rwLock.EnterWriteLock();
            if (!_cache.TryGetValue(key, out value))
            {
                try
                {
                    value = Create(key);
                    _cache[key] = value;
                }
                finally
                {
                    _rwLock.ExitWriteLock();
                }
            }

            return value;
        }

        protected abstract TValue Create(TKey key);
    }

    public class ConstructorInvokerCache : FastReflectionCache<ConstructorInfo, IConstructorInvoker>
    {
        protected override IConstructorInvoker Create(ConstructorInfo key) => FastReflectionFactories.ConstructorInvokerFactory.Create(key);
    }

    public class MethodInvokerCache : FastReflectionCache<MethodInfo, IMethodInvoker>
    {
        protected override IMethodInvoker Create(MethodInfo key) => FastReflectionFactories.MethodInvokerFactory.Create(key);
    }

    public class FieldAccessorCache : FastReflectionCache<FieldInfo, IFieldAccessor>
    {
        protected override IFieldAccessor Create(FieldInfo key) => FastReflectionFactories.FieldAccessorFactory.Create(key);
    }

    public class PropertyAccessorCache : FastReflectionCache<PropertyInfo, IPropertyAccessor>
    {
        protected override IPropertyAccessor Create(PropertyInfo key) => FastReflectionFactories.PropertyAccessorFactory.Create(key);
    }

    public class TypesCache : FastReflectionCache<Type, TypeInfoCache>
    {
        protected override TypeInfoCache Create(Type key) => new TypeInfoCache(key);
    }
}
