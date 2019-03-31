namespace Disassembler.Win16
{
    public enum RelocSource : byte
    {
        LowByte = 0,
        Segment = 2,
        FarAddress = 3,
        Offset = 5,
    }
}
