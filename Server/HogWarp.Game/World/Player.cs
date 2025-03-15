namespace HogWarp.Game.World;

public interface Player
{
    public void Kick();

    public ulong Id
    {
        get;
    }

    public uint ConnectionId
    {
        get;
    }

    public string Username
    {
        get;
    }

    public string UniqueId
    {
        get;
    }

    public Actor? Puppet
    {
        get;
    }
}