namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ImportOrdinal
    {
        public ushort Index;
        public ushort Ordinal;
    }
}
