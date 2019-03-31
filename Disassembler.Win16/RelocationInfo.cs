namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct RelocationInfo
    {
        [FieldOffset(0)]
        public RelocSource SourceType;
        [FieldOffset(1)]
        public RelocFlags Flags;
        [FieldOffset(2)]
        public ushort Offset;
        [FieldOffset(4)]
        public InternalRef InternalRef;
        [FieldOffset(4)]
        public ImportName ImportName;
        [FieldOffset(4)]
        public ImportOrdinal ImportOrdinal;
        [FieldOffset(4)]
        public OSFixup OSFixup;
    }
}
