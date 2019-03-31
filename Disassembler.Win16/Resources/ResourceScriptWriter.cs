namespace Disassembler.Win16.Resources
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResourceScriptWriter
    {
        public string Path { get; set; }
        public Win16Executable Executable { get; private set; }

        public ResourceScriptWriter(Win16Executable executable, string path)
        {
            this.Executable = executable;
            this.Path = path;
        }

        public void WriteScript()
        {
            string resourceFilename = Executable.ResidentNames[0].Name + ".TXT";
            using (IndentedWriter writer = new IndentedWriter(new StreamWriter(Path + "/" + resourceFilename)))
            {
                foreach (ResourceGroup rg in Executable.ResourceTable)
                {
                    WriteResource(writer, rg);
                    writer.WriteLine();
                }
            }
        }

        private void WriteResource(IndentedWriter writer, ResourceGroup resGroup)
        {
            switch (resGroup.ResType)
            {
                case ResourceType.CursorDirectory:
                    WriteCursor(writer, resGroup);
                    break;
                case ResourceType.IconDirectory:
                    WriteIcon(writer, resGroup);
                    break;
                case ResourceType.Bitmap:
                    WriteBitmap(writer, resGroup);
                    break;
                case ResourceType.Menu:
                    WriteMenu(writer, resGroup);
                    break;
                case ResourceType.Dialog:
                    WriteDialog(writer, resGroup);
                    break;
                case ResourceType.Accelerator:
//                    WriteAccelerator(writer, resGroup);
                    break;
                case ResourceType.String:
                    WriteStringTable(writer, resGroup);
                    break;
                case ResourceType.ResourceData:
//                    WriteResourceData(writer, resGroup);
                    break;
                case ResourceType.Icon:
                case ResourceType.Cursor:
                case ResourceType.Font:
                    Console.WriteLine("Skipping {0}, resource defined by {0}Directory instead.", resGroup.ResType);
                    break;
                case ResourceType.FontDirectory:
                case ResourceType.NameTable:
                case ResourceType.VersionInfo:
                    Console.WriteLine("Skipping {0}, not implemented yet.", resGroup.ResType);
                    break;
                case ResourceType.None:
                    Console.WriteLine("Encountered {0}, which should not happen.", resGroup.ResType);
                    break;
                default:
//                    WriteCustomResource(writer, resGroup);
                    break;
            }
        }

        private void WriteCursor(IndentedWriter writer, ResourceGroup resGroup)
        {
            foreach (Resource res in resGroup.Resources)
            {
                string name = res.Name == string.Empty ? res.Id.ToString() : res.Name;
                string filename = res.Name == string.Empty ? res.Id.ToString("X4") : res.Name;
                writer.WriteLine("{0}\tCURSOR {1}.cur", name, filename);
            }
        }

        private void WriteIcon(IndentedWriter writer, ResourceGroup resGroup)
        {
            foreach (Resource res in resGroup.Resources)
            {
                string name = res.Name == string.Empty ? res.Id.ToString() : res.Name;
                string filename = res.Name == string.Empty ? res.Id.ToString("X4") : res.Name;
                writer.WriteLine("{0}\tICON {1}.ico", name, filename);
            }
        }

        private void WriteBitmap(IndentedWriter writer, ResourceGroup resGroup)
        {
            foreach (Resource res in resGroup.Resources)
            {
                string name = res.Name == string.Empty ? res.Id.ToString() : res.Name;
                string filename = res.Name == string.Empty ? res.Id.ToString("X4") : res.Name;
                writer.WriteLine("{0}\tBITMAP {1}.bmp", name, filename);
            }
        }

        private void WriteMenu(IndentedWriter writer, ResourceGroup resGroup)
        {
            foreach (Resource res in resGroup.Resources)
            {
                string name = res.Name == string.Empty ? res.Id.ToString() : res.Name;

                Menu menu = Menu.Load(res.Data);
                writer.WriteLine("{0}\tMENU", name);
                writer.WriteLine("BEGIN");
                writer.Indent++;
                foreach (MenuItem menuItem in menu.Items)
                {
                    WriteMenuItem(writer, menuItem);
                }
                writer.Indent--;
                writer.WriteLine("END");
                writer.WriteLine();
            }
        }

        private void WriteMenuItem(IndentedWriter writer, MenuItem menuItem)
        {
            if ((menuItem.Flags & MenuFlags.Popup) == 0)
            {
                NormalMenuItem item = (NormalMenuItem)menuItem;
                if (item.Flags == 0)
                {
                    writer.WriteLine("MENUITEM \"{0}\", {1}", item.Name, item.Id);
                }
                else
                {
                    string flags = item.Flags.ToString().ToUpper();
                    writer.WriteLine("MENUITEM \"{0}\", {1}, {2}", item.Name, item.Id, flags);
                }
            }
            else
            {
                if ((menuItem.Flags & ~MenuFlags.Popup) == 0)
                {
                    writer.WriteLine("POPUP \"{0}\"", menuItem.Name);
                }
                else
                {
                    string flags = (menuItem.Flags & ~MenuFlags.Popup).ToString().ToUpper();
                    writer.WriteLine("POPUP \"{0}\", {1}", menuItem.Name, flags);
                }

                writer.WriteLine("BEGIN");
                writer.Indent++;
                PopupMenuItem popup = (PopupMenuItem)menuItem;
                foreach (MenuItem item in popup.Items)
                {
                    WriteMenuItem(writer, item);
                }
                writer.Indent--;
                writer.WriteLine("END");
            }
        }

        private void WriteDialog(IndentedWriter writer, ResourceGroup resGroup)
        {
            foreach (Resource res in resGroup.Resources)
            {
                string name = res.Name == string.Empty ? res.Id.ToString() : res.Name;
                Dialog dialog = Dialog.Load(res.Data);
                writer.WriteLine("{0}\tDIALOG {1}, {2}, {3}, {4}", name, dialog.X, dialog.Y, dialog.Width, dialog.Height);
                writer.WriteLine("STYLE {0}", dialog.Style.ToString().ToUpper());

                if (!string.IsNullOrEmpty(dialog.MenuName) || dialog.MenuId != 0)
                {
                    string menuName = dialog.MenuName == string.Empty ? dialog.MenuId.ToString() : dialog.MenuName;
                    writer.WriteLine("MENU \"{0}\"", menuName);
                }

                if (!string.IsNullOrEmpty(dialog.ClassName))
                {
                    writer.WriteLine("CLASS \"{0}\"", dialog.ClassName);
                }

                if (!string.IsNullOrEmpty(dialog.Caption))
                {
                    writer.WriteLine("CAPTION \"{0}\"", dialog.Caption);
                }

                if ((dialog.Style & ControlStyle.SetFont) == ControlStyle.SetFont)
                {
                    writer.WriteLine("FONT {0}, \"{1}\"", dialog.FontSize, dialog.FontName);
                }

                writer.WriteLine("BEGIN");
                writer.Indent++;
                foreach (Control control in dialog.Controls)
                {
                    WriteControl(writer, control);
                }
                writer.Indent--;
                writer.WriteLine("END");
                writer.WriteLine();
            }
        }

        private void WriteControl(IndentedWriter writer, Control control)
        {
            switch (control.Type)
            {
                case ControlType.Custom:
                    ScriptHelper.WriteControl(writer, control);
                    //writer.WriteLine("CONTROL \"{0}\", {1}, {2}, {3}, {4}, {5}, {6}, {7}", control.Text, control.Id, control.Name, control.Style, control.X, control.Y, control.Width, control.Height);
                    break;
                case ControlType.Static:
                    switch ((int)control.Style & 0x7F)
                    {
                        case 0:
                            writer.WriteLine("LTEXT \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 1:
                            writer.WriteLine("CTEXT \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 2:
                            writer.WriteLine("RTEXT \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 3:
                            writer.WriteLine("ICON \"{0}\", {1}, {2}, {3}", control.Text, control.Id, control.X, control.Y);
                            break;
                        default:
                            ScriptHelper.WriteControl(writer, control);
//                            writer.WriteLine("CONTROL \"{0}\", {1}, \"{2}\", {3}, {4}, {5}, {6}, {7}", control.Text, control.Id, control.Type, FlagParser.GetFlags((StaticStyle)control.Style), control.X, control.Y, control.Width, control.Height);
                            break;
                    }
                    break;
                case ControlType.Button:
                    switch ((int)control.Style & 15)
                    {
                        case 0:
                            writer.WriteLine("PUSHBUTTON \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 1:
                            writer.WriteLine("DEFPUSHBUTTON \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 2:
//                        case 3:
                            writer.WriteLine("CHECKBOX \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 4:
//                        case 9:
                            writer.WriteLine("RADIOBUTTON \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        case 7:
                            writer.WriteLine("GROUPBOX \"{0}\", {1}, {2}, {3}, {4}, {5}", control.Text, control.Id, control.X, control.Y, control.Width, control.Height);
                            break;
                        default:
                            writer.WriteLine("CONTROL \"{0}\", {1}, \"{2}\", {3}, {4}, {5}, {6}, {7}", control.Text, control.Id, control.Type, FlagParser.GetFlags((ButtonStyle)control.Style), control.X, control.Y, control.Width, control.Height);
                            break;
                    }
                    break;
                default:
                    ScriptHelper.WriteControl(writer, control);
                //{
                //    string className = control.Type == ControlType.Custom ? control.Name : control.Type.ToString();
                //    writer.WriteLine("CONTROL \"{0}\", {1}, {2}, {3}, {4}, {5}, {6}, {7}", control.Text, control.Id, className, control.Style, control.X, control.Y, control.Width, control.Height);
                //}
                    break;
            }
        }

        private void WriteStringTable(IndentedWriter writer, ResourceGroup resGroup)
        {
            writer.WriteLine("STRINGTABLE");
            writer.WriteLine("BEGIN");
            writer.Indent++;
            foreach (Resource res in resGroup.Resources)
            {
                StringBundle bundle = StringBundle.Load(res.Data);
                for (int i = 0; i < bundle.Strings.Length; i++)
                {
                    if (bundle.Strings[i] != string.Empty)
                    {
                        writer.WriteLine("{0}, \"{1}\"", i + (res.Id << 4), bundle.Strings[i]);
                    }
                }
            }
            writer.Indent--;
            writer.WriteLine("END");
        }

        private static class ScriptHelper
        {
            public static void WriteControl(IndentedWriter writer, Control c) =>
                writer.WriteLine($"CONTROL \"{c.Text}\", {c.Id}, \"{(c.Type == ControlType.Custom ? c.Name : c.Type.ToString())}\", {FlagParser.GetFlags(c)}, {c.X}, {c.Y}, {c.Width}, {c.Height}");
        }

        private static class FlagParser
        {
            public static string GetFlags(Control c)
            {
                switch (c.Type)
                {
                    case ControlType.Button:
                        return GetFlags((ButtonStyle)c.Style);
                    case ControlType.Static:
                        return GetFlags((StaticStyle)c.Style);
                    case ControlType.Custom:
                        return c.Style.ToString("x");
                }
                return string.Empty;
            }

            public static string GetFlags(StaticStyle flags)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append(flags & (StaticStyle)0xF);

                if ((flags & StaticStyle.SS_NOPREFIX) == StaticStyle.SS_NOPREFIX)
                {
                    builder.AppendFormat(" | {0}", StaticStyle.SS_NOPREFIX);
                }

                if ((flags & StaticStyle.WS_TABSTOP) == StaticStyle.WS_TABSTOP)
                {
                    builder.AppendFormat(" | {0}", StaticStyle.WS_TABSTOP);
                }

                if ((flags & StaticStyle.WS_GROUP) == StaticStyle.WS_GROUP)
                {
                    builder.AppendFormat(" | {0}", StaticStyle.WS_GROUP);
                }
                return builder.ToString();
            }

            public static string GetFlags(ButtonStyle flags)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append(flags & (ButtonStyle)0xF);

                if ((flags & ButtonStyle.WS_TABSTOP) == ButtonStyle.WS_TABSTOP)
                {
                    builder.AppendFormat(" | {0}", ButtonStyle.WS_TABSTOP);
                }

                if ((flags & ButtonStyle.WS_GROUP) == ButtonStyle.WS_GROUP)
                {
                    builder.AppendFormat(" | {0}", ButtonStyle.WS_GROUP);
                }
                return builder.ToString();
            }
        }

        enum StaticStyle
        {
            SS_LEFT = 0,
            SS_CENTER = 1,
            SS_RIGHT = 2,
            SS_ICON = 3,
            SS_BLACKRECT = 4,
            SS_GRAYRECT = 5,
            SS_WHITERECT = 6,
            SS_BLACKFRAME = 7,
            SS_GRAYFRAME = 8,
            SS_WHITEFRAME = 9,
            SS_SIMPLE = 0xB,
            SS_LEFTNOWORDWRAP = 0xC,

            SS_NOPREFIX = 0x80,

            WS_TABSTOP = 0x10000,
            WS_GROUP = 0x20000,
        }

        enum ButtonStyle
        {
            BS_PUSHBUTTON = 0,
            BS_DEFPUSHBUTTON = 1,
            BS_CHECKBOX = 2,
            BS_AUTOCHECKBOX = 3,
            BS_RADIOBUTTON = 4,
            BS_3STATE = 5,
            BS_AUTO3STATE = 6,
            BS_GROUPBOX = 7,
            BS_USERBUTTON = 8,
            BS_AUTORADIOBUTTON = 9,

            BS_OWNERDRAW = 0xB,
            BS_LEFTTEXT = 0x20,

            WS_TABSTOP = 0x10000,
            WS_GROUP = 0x20000,
        }

    }
}
