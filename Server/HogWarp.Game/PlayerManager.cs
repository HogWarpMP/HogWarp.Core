namespace HogWarp.Game
{
    public abstract class PlayerManager
    {
        public abstract IEnumerable<Player> Players { get; }
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
    }
}
