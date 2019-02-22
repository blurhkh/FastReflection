﻿namespace FastReflection.BaseAccessor
{
    public interface IPropertyAccessor
    {
        object GetValue(object instance);

        void SetValue(object instance, object value);
    }
}