namespace HogWarp.Game.World.Components;

public struct MovementComponent : IComponent
{
    public ulong Tick;
    public global::System.Numerics.Vector3 Position;
    public float Direction;
    public float Speed;
    public bool IsInAir;
}