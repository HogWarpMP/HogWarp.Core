using HogWarp.Game.System;
using HogWarp.Game.World;
using Timer = HogWarp.Game.System.Timer;

namespace HogWarp.Game;

public class Server
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    static public Level Level { get; internal set; }
    static public Timer Timer { get; internal set; }
    static public RpcManager RpcManager { get; internal set; }
    static public PluginManager PluginManager { get; internal set; }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
