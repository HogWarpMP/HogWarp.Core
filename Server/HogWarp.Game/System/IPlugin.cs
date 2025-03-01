namespace HogWarp.Game.System;

public interface IPlugin
{
    string Author { get; }
    string Name { get; }
    Version Version { get; }

    void PostLoad();
    void Shutdown();
}
