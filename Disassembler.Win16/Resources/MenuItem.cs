namespace Disassembler.Win16.Resources
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public MenuFlags Flags;
        public string Name;
    }

    public class NormalMenuItem : MenuItem
    {
        public int Id;
    }

    public class PopupMenuItem : MenuItem
    {
        public List<MenuItem> Items;
    }
}
