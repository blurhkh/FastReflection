using System;
using System.Collections.Generic;
using System.Text;

namespace FastReflection.Caches
{
    public interface IFastReflectionCache<TKey, TValue>
    {
        TValue Get(TKey key);
    }
}
