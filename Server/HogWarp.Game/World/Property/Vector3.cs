using HogWarp.Game.System;

namespace HogWarp.Game.World.Property;

public static class Vector3Extension
{
    public static void Serialize(this global::System.Numerics.Vector3 vector, IBuffer writer)
    {
        writer.WriteFloat(vector.X);
        writer.WriteFloat(vector.Y);
        writer.WriteFloat(vector.Z);
    }

    public static void Deserialize(this global::System.Numerics.Vector3 vector, IBuffer reader)
    {
        vector.X = reader.ReadFloat();
        vector.Y = reader.ReadFloat();
        vector.Z = reader.ReadFloat();
    }
}
