namespace Disassembler.Win16
{
    using System;

    [Flags]
    public enum ResourceFlags : int
    {
        Moveable = 0x10,
        Pure = 0x20,
        Preload = 0x40,
    }
}
