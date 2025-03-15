namespace HogWarp.Game.World;

public abstract class Character
{
    public abstract ulong Id
    {
        get;
    }

    // Where do we put shared types? In this module?
    public abstract global::System.Numerics.Vector3 Position
    {
        get;
    }

    public abstract float Direction
    {
        get;
    }

    public abstract float Speed
    {
        get;
    }

    public abstract bool IsInAir
    {
        get;
    }

    public abstract byte House
    {
        get;
    }

    public abstract byte Gender
    {
        get;
    }

    public abstract bool IsMounted
    {
        get;
    }
}