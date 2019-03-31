namespace Disassembler.Win16
{
    public enum SegmentFlags : ushort
    {
        Code = 0,
        Data = 1,
        Moveable = 16,
        Shareable = 32,
        Preload = 64,
        ExecuteReadOnly = 128,
        RelocInfo = 256,
        DebugInfo = 512,
    }
}
