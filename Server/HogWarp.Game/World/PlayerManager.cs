using System.Collections;

namespace HogWarp.Game.World;

public abstract class PlayerManager : IEnumerable<Player>
{    
    public abstract Player? GetById(ulong id);

    public delegate void PlayerEvent(Player Id);

    public event PlayerEvent? PlayerJoinEvent;
    public event PlayerEvent? PlayerLeftEvent;

    protected void OnPlayerJoin(Player player)
    {
        PlayerJoinEvent?.Invoke(player);
    }

    protected void OnPlayerLeft(Player player)
    {
        PlayerLeftEvent?.Invoke(player);
    }

    public abstract IEnumerator<Player> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
