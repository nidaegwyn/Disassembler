namespace Disassembler.Win16
{
    public struct Segment
    {
        public byte[] Data;
        public RelocationInfo[] Relocations;
    }
}
