namespace Disassembler.Win16
{
    public enum ResourceType : ushort
    {
        None = 0,
        Cursor = 1,
        Bitmap = 2,
        Icon = 3,
        Menu = 4,
        Dialog = 5,
        String = 6,
        FontDirectory = 7,
        Font = 8,
        Accelerator = 9,
        ResourceData = 10,
        CursorDirectory = 12,
        IconDirectory = 14,
        NameTable = 15,
        VersionInfo = 16,
    }
}
