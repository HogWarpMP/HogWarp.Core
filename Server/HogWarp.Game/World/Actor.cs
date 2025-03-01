namespace HogWarp.Game.World;

public class Actor
{
    protected uint _id;
    protected ulong _klass;

    public uint Id { get { return _id; } set { _id = value; } }
    public ulong Klass { get { return _klass; } set { _klass = value; } }

    public Actor()
    {
    }
}
