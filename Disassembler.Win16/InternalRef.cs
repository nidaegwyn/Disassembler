namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct InternalRef
    {
        public byte Seg;
        public byte Zero;
        public ushort Offset;
    }
}
