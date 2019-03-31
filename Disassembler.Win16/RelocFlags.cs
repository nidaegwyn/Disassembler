namespace Disassembler.Win16
{
    public enum RelocFlags : byte
    {
        InternalRef = 0,
        ImportOrdinal = 1,
        ImportName = 2,
        OSFixup = 3,
        AdditiveOffset = 4,
    }
}
