namespace Disassembler.Win16.Resources
{
    public class Control
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Id;
        public ControlStyle Style;
        public ControlType Type;
        public string Name;
        public string Text;

        public static Control Load(byte[] bytes, ref int offset)
        {
            Control control = new Control();

            control.X = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            control.Y = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            control.Width = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            control.Height = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            control.Id = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
            offset += 2;
            if (control.Id == 65535) control.Id = -1;
            control.Style = (ControlStyle)ByteArrayUtils.ByteArrayToUInt(bytes, offset);
            offset += 4;

            int classType = bytes[offset];
            if ((classType & 0x80) == 0x80)
            {
                control.Type = (ControlType)classType;
                offset++;
            }
            else
            {
                control.Type = ControlType.Custom;
                control.Name = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
                offset += control.Name.Length + 1;
            }

            control.Text = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
            offset += control.Text.Length + 1;

            offset++; // for some reason??

            return control;
        }
    }
}
