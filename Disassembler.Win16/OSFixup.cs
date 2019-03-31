namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct OSFixup
    {
        public ushort FixupType;
        public ushort Zero;
    }
}
