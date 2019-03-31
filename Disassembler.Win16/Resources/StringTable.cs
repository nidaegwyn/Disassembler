namespace Disassembler.Win16.Resources
{
    public class StringBundle
    {
        public string[] Strings;

        public static StringBundle Load(byte[] bytes)
        {
            StringBundle bundle = new StringBundle();
            bundle.Strings = new string[16];
            int offset = 0;

            for (int i = 0; i < 16; i++)
            {
                bundle.Strings[i] = ByteArrayUtils.ByteArrayToString(bytes, offset);
                offset += bundle.Strings[i].Length + 1;
            }

            return bundle;
        }
    }
}
