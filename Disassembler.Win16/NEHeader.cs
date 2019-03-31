namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NEHeader
    {
        public ushort Signature;
        public byte LinkerVersion;
        public byte LinkerRevision;
        public ushort EntryTableOffset;
        public ushort EntryTableSize;
        public uint CRC_32;
        public byte ProgFlags;
        public byte AppFlags;
        public ushort AutoDataSegment;
        public ushort InitialHeapSize;
        public ushort InitialStackSize;
        public ushort InitialIP;
        public ushort InitialCS;
        public ushort InitialSP;
        public ushort InitialSS;
        public ushort NumSegments;
        public ushort NumModules;
        public ushort NonResidentNameSize;
        public ushort SegmentTableOffset;
        public ushort ResourceTableOffset;
        public ushort ResidentNameTableOffset;
        public ushort ModuleTableOffset;
        public ushort ImportNameTableOffset;
        public uint NonResidentNameTableOffset;
        public ushort NumMoveable;
        public ushort SectorAlignment;
        public ushort NumResources;
        public byte TargetOS;
        public byte OSExtraFlags;
        public ushort GangloadOffset;
        public ushort GangloadSectorCount;
        public ushort MinCodeSwap;
        public byte ExpectedWindowsVersionMinor;
        public byte ExpectedWindowsVersionMajor;
    }
}
