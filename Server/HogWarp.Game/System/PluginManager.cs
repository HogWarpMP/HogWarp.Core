namespace HogWarp.Game.System;

public abstract class PluginManager
{
    public T? Get<T>()
    {
        return (T?)Get(typeof(T));
    }

    public abstract IPlugin? Get(Type type);
}