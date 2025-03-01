namespace HogWarp.Game.System;

public interface IBuffer
{
    public string ReadString();
    public sbyte ReadInt8();
    public byte ReadUint8();
    public short ReadInt16();
    public ushort ReadUint16();
    public int ReadInt32();
    public uint ReadUint32();
    public long ReadInt64();
    public ulong ReadUint64();
    public ulong ReadVarInt();
    public float ReadFloat();
    public double ReadDouble();
    public bool ReadBool();
    public void WriteString(string Str);
    public void WriteInt8(sbyte Data);
    public void WriteUint8(byte Data);
    public void WriteInt16(short Data);
    public void WriteUint16(ushort Data);
    public void WriteInt32(int Data);
    public void WriteUint32(uint Data);
    public void WriteInt64(long Data);
    public void WriteUint64(ulong Data);
    public void WriteVarInt(ulong Data);
    public void WriteFloat(float Data);
    public void WriteDouble(int Data);
    public void WriteBool(bool Data);
}
