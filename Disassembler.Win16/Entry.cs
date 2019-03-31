namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Entry
    {
        public EntryType Type;
        public EntryFlags Flags;
        public byte Segment;
        public ushort Offset;
    }
}
