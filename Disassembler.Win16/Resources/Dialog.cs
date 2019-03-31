namespace Disassembler.Win16.Resources
{
    using System.Collections.Generic;

    public class Dialog
    {
        public ControlStyle Style;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int MenuId;
        public string MenuName;
        public string ClassName;
        public string Caption;
        public int FontSize;
        public string FontName;
        public List<Control> Controls;

        public static Dialog Load(byte[] bytes)
        {
            Dialog dialog = new Dialog();
            int offset = 0;

            dialog.Style = (ControlStyle)ByteArrayUtils.ByteArrayToUInt(bytes, offset);
            offset += 4;
            int controlCount = bytes[offset];
            offset++;
            dialog.X = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            dialog.Y = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            dialog.Width = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            dialog.Height = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;

            if (bytes[offset] == 0xFF)
            {
                offset++;
                dialog.MenuId = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
                offset += 2;
            }
            else
            {
                dialog.MenuName = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
                offset += dialog.MenuName.Length + 1;
            }

            dialog.ClassName = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
            offset += dialog.ClassName.Length + 1;
            dialog.Caption = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
            offset += dialog.Caption.Length + 1;

            if ((dialog.Style & ControlStyle.SetFont) == ControlStyle.SetFont)
            {
                dialog.FontSize = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
                offset += 2;
                dialog.FontName = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
                offset += dialog.FontName.Length + 1;
            }

            dialog.Controls = new List<Control>();
            for (int i = 0; i < controlCount; i++)
            {
                dialog.Controls.Add(Control.Load(bytes, ref offset));
            }

            return dialog;
        }
    }
}
