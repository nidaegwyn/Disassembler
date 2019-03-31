﻿namespace Disassembler.Win16.Resources
{
    using System;

    [Flags]
    public enum ControlStyle
    {
        SetFont = 0x40,
        ModalFrame = 0x80,

        MaximizeBox = 0x00010000,
        MinimizeBox = 0x00020000,
        ThickFrame = 0x00040000,
        SysMenu = 0x00080000,
        HScroll = 0x00100000,
        VScroll = 0x00200000,
        DlgFrame = 0x00400000,
        Border = 0x00800000,
        Caption = 0x00C00000,

        Minimize = 0x20000000,
        Maximize = 0x01000000,

        Disabled = 0x08000000,
        Visible = 0x10000000,

        ClipChildren = 0x02000000,
        ClipSiblings = 0x04000000,

        Child = 0x40000000,
        Popup = unchecked((int)0x80000000),
    }
}
