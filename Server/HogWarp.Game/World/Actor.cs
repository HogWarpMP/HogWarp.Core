using HogWarp.Game.World.Components;

namespace HogWarp.Game.World;

public interface Actor
{
    public ulong Id { get; }

    public bool HasComponent<T>() where T : IComponent;

    public T? GetComponent<T>() where T : IComponent;
}
