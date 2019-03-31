namespace Disassembler.Win16
{
    public struct ResourceGroup
    {
        public int TypeId;
        public ResourceType ResType;
        public string TypeName;
        public Resource[] Resources;
    }
}
