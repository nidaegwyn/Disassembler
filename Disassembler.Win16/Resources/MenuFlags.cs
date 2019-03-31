namespace Disassembler.Win16.Resources
{
    using System;

    [Flags]
    public enum MenuFlags
    {
        Grayed = 0x01,
        Disabled = 0x02,
        Checked = 0x08,
        Popup = 0x10,
        MenuBarBreak = 0x20,
        MenuBreak = 0x40,
        End = 0x80,
        Help = 0x4000,
    }
}
