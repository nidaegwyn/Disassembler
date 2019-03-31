namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SegmentEntry
    {
        public ushort Offset;
        public ushort Length;
        public SegmentFlags Flags;
        public ushort MinAllocationSize;
    }
}
