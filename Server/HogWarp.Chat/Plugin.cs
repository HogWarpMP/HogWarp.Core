using HogWarp.Replicated;
using HogWarpSdk.Game;
using HogWarpSdk.Systems;
using System.Numerics;

namespace HogWarp.Chat
{
    public class Plugin : HogWarpSdk.IPlugin
    {
        private Logger log = new Logger("HogWarpChat");
        public event Action<Player, string> OnChatMessage;
        private float sayDist = 400;
        private float shoutDist = 1500;
        private float whisperDist = 100;
        private BP_HogWarpChat? chatActor;
        public bool chatMsgOverride = false;
        private Dictionary<string, Action<Player, string>> commands = new Dictionary<string, Action<Player, string>>();

        public Plugin()
        {
            OnChatMessage += Chat_OnChatMessage;


        }

        public string Author => "HogWarp Team";

        public string Name => "HogWarpChat";

        public Version Version => new(1, 0);

        public void PostLoad()
        {
            chatActor = HogWarpSdk.Server.World.Spawn<BP_HogWarpChat>()!;
            chatActor.Plugin = this;

            commands.Add("/me", SlashMe);
            commands.Add("/house", SlashHouse);
            commands.Add("/say", SlashDistMsg);
            commands.Add("/shout", SlashDistMsg);
            commands.Add("/whisper", SlashDistMsg);
        }

        public void Shutdown()
        {
        }

        enum House
        {
            Gryffindor,
            Hufflepuff,
            Ravenclaw,
            Slytherin,
            Unaffiliated
        }

        public void AddCommand(string command, Action<Player, string> action)
        {
            if (!commands.TryAdd(command, action))
            {
                log.Warn($"{command} command already exists!");
            }
        }

        public void ReceiveMessage(Player player, string msg)
        {
            if (OnChatMessage != null)
                OnChatMessage.Invoke(player, msg);
        }

        public void SendMessage(Player player, string msg)
        {
            if (chatActor != null)
                chatActor.RecieveMsg(player, msg);
        }

        private void SlashMe(Player player, string msg)
        {
            foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
            {
                SendMessage(p, "<Server>" + player.Username + msg.Substring(3) + "</>");
            }
        }

        private void SlashHouse(Player player, string msg)
        {
            foreach (var p in HogWarpSdk.Server.PlayerSystem.Players.Where(p => p.House == player.House))
            {
                SendMessage(p, "<img id=\"" + (House)player.House + "\"/><" + (House)player.House + ">" + player.Username + ": " + msg.Substring(7) + "</>");
            }
        }

        private void SlashDistMsg(Player player, string msg)
        {
            float msgDist = 0;
            int msgSub = 0;
            string msgType = "says";

            if (msg.StartsWith("/say")) { msgDist = sayDist; msgSub = 5; msgType = "says"; }
            else if (msg.StartsWith("/shout")) { msgDist = shoutDist; msgSub = 7; msgType = "shouts"; }
            else { msgDist = whisperDist; msgSub = 9; msgType = "whispers"; }

            foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
            {
                Vector3 plyPos = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);
                Vector3 pPos = new Vector3(p.Position.X, p.Position.Y, p.Position.Z);
                var dist = plyPos - pPos;

                if (dist.Length() <= msgDist)
                {
                    SendMessage(p, player.Username + " " + msgType + ": " + msg.Substring(msgSub));
                }
            }
        }

        private void BuildMessage(Player player, string msg)
        {
            log.Info(player.Username + ": " + msg);

            if (commands.ContainsKey(msg.Split(" ")[0]))
            {
                commands[msg.Split(" ")[0]].DynamicInvoke(player, msg);
            }
            else
            {
                foreach (var p in HogWarpSdk.Server.PlayerSystem.Players)
                {
                    SendMessage(p, "<img id=\"" + (House)player.House + "\"/><" + (House)player.House + ">" + player.Username + ": </>" + msg);
                }
            }
        }
        private void Chat_OnChatMessage(Player player, string msg)
        {
            if (!chatMsgOverride)
                BuildMessage(player, msg);
        }
    }
}

namespace HogWarp.Replicated
{
    public partial class BP_HogWarpChat
    {
        internal Chat.Plugin? Plugin { get; set; }
        public partial void SendMsg(Player player, string Message)
        {
            Plugin!.ReceiveMessage(player, Message);
        }
    }
}
