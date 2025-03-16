using System.Collections;

namespace HogWarp.Game.World;

public abstract class PlayerManager : IEnumerable<Player>
{    
    public abstract Player? GetById(ulong id);

    public delegate void PlayerDelegate(Player Id);
    public delegate bool AuthenticationDelegate(string token, string username, ref bool block);

    public event PlayerDelegate? PlayerJoinEvent;
    public event PlayerDelegate? PlayerLeftEvent;
    public event AuthenticationDelegate? AuthenticationEvent;

    protected void OnPlayerJoin(Player player)
    {
        PlayerJoinEvent?.Invoke(player);
    }

    protected void OnPlayerLeft(Player player)
    {
        PlayerLeftEvent?.Invoke(player);
    }

    protected void OnAuthentication(string token, string username, ref bool block)
    {
        AuthenticationEvent?.Invoke(token, username, ref block);
    }

    public abstract IEnumerator<Player> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
