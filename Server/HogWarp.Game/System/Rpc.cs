namespace HogWarp.Game.System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ServerRpcAttribute : Attribute
{
    public string Function = "";
    public ulong Hash = 0;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ClientRpcAttribute : Attribute
{
    public string Function = "";
    public ulong Hash = 0;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ReplicatedAttribute : Attribute
{
    public string Class = "";
    public ulong Hash = 0;
}

public abstract class RpcManager
{
    public abstract void Call(ulong playerId, uint Actor, ulong klass, ulong func, IBuffer args);
    public abstract IBuffer Create();
    public abstract void Release(IBuffer buffer);
}
