namespace HogWarp.Game.World;

public abstract class Level
{
    public delegate void UpdateDelegate(float Delta);

    public event UpdateDelegate? UpdateEvent;

    public abstract PlayerManager Players { get; }

    protected void OnUpdate(float Delta)
    {
        UpdateEvent?.Invoke(Delta);
    }

    protected abstract ScriptActor? SpawnActorByClass(Type type);

    public T? Spawn<T>() where T : ScriptActor
    {
        return (T?)SpawnActorByClass(typeof(T));
    }

    public abstract void Destroy(ScriptActor actor);
}
