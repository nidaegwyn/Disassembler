namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MZHeader
    {
        public ushort Signature;
        public ushort ExtraBytes;
        public ushort Pages;
        public ushort RelocationCount;
        public ushort HeaderSize;
        public ushort MinimumAllocation;
        public ushort MaximumAllocation;
        public ushort InitialSS;
        public ushort InitialSP;
        public ushort Checksum;
        public ushort InitialIP;
        public ushort InitialCS;
        public ushort RelocationTableOffset;
        public ushort OverlayNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Reserved1;
        public ushort OemId;
        public ushort OemInfo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[] Reserved2;
        public int NewHeaderOffset;
    }
}
