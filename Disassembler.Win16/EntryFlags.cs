namespace Disassembler.Win16
{
    using System;

    [Flags]
    public enum EntryFlags : byte
    {
        Exported = 0x01,
        Global = 0x02,
    }
}
