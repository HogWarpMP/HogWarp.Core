using HogWarp.Game.World.Data;

namespace HogWarp.Game.World.Components;

public struct CharacterCustomizationComponent : IComponent
{
    public byte HouseId;
    public byte Gender;
    public string Name;
    public GearItem[] Gear;
    public AvatarPreset[] Presets;
}