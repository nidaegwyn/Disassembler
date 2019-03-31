namespace Disassembler.Win16.Resources
{
    using System.Collections.Generic;

    public class Menu
    {
        public string Name;
        public ResourceFlags Flags;
        public List<MenuItem> Items;

        public static Menu Load(byte[] bytes)
        {
            Menu menu = new Menu();
            int offset = 4; // skip header
            menu.Items = ParseMenuItemList(bytes, ref offset);
            return menu;
        }

        private static List<MenuItem> ParseMenuItemList(byte[] bytes, ref int offset)
        {
            List<MenuItem> items = new List<MenuItem>();
            MenuFlags flags = 0;

            do
            {
                flags = (MenuFlags)ByteArrayUtils.ByteArrayToUShort(bytes, offset);
                offset += 2;

                switch (flags & MenuFlags.Popup)
                {
                    case MenuFlags.Popup:
                    {
                        PopupMenuItem menuItem = new PopupMenuItem();
                        menuItem.Flags = flags & ~MenuFlags.End;
                        menuItem.Name = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
                        offset += menuItem.Name.Length + 1;
                        menuItem.Items = ParseMenuItemList(bytes, ref offset);
                        items.Add(menuItem);
                    }
                    break;
                    default:
                    {
                        NormalMenuItem menuItem = new NormalMenuItem();
                        menuItem.Flags = flags & ~MenuFlags.End;
                        menuItem.Id = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
                        offset += 2;
                        menuItem.Name = ByteArrayUtils.ByteArrayToStringZ(bytes, offset);
                        offset += menuItem.Name.Length + 1;
                        items.Add(menuItem);
                    }
                    break;
                }
            }
            while ((flags & MenuFlags.End) != MenuFlags.End);

            return items;
        }
    }
}
