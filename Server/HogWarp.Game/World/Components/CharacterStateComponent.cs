namespace HogWarp.Game.World.Components;

public struct CharacterStateComponent : IComponent
{
    public bool Hooded;
    public bool Mounted;
    public bool WandDrawn;
    public byte MountType;
    public string MountName;
    public string WandStyle;
}