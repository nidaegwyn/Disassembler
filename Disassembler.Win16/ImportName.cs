namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ImportName
    {
        public ushort Index;
        public ushort Offset;
    }
}
