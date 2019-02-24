namespace FastReflection.BaseInvoker
{
    public interface IMethodInvoker
    {
        object Invoke(object instance, params object[] parameters);
    }
}