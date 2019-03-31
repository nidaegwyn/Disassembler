namespace Disassembler.Win16
{
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ByteArrayUtils
    {
        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public static T ByteArrayToStructure<T>(byte[] bytes, int offset) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject() + offset);
            }
            finally
            {
                handle.Free();
            }
        }

        public static T[] ByteArrayToStructureArray<T>(byte[] bytes, int offset, int count) where T : struct
        {
            T[] array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = ByteArrayToStructure<T>(bytes, offset + i * Marshal.SizeOf(typeof(T)));
            }

            return array;
        }

        public static string ByteArrayToString(byte[] bytes, int offset, int length)
        {
            return Encoding.ASCII.GetString(bytes, offset, length);
        }

        public static string ByteArrayToStringZ(byte[] bytes, int offset)
        {
            int index = System.Array.IndexOf<byte>(bytes, 0, offset);
            int length = index - offset;
            return ByteArrayToString(bytes, offset, length);
        }

        public static string ByteArrayToString(byte[] bytes, int offset)
        {
            int length = bytes[offset];
            return ByteArrayToString(bytes, offset + 1, length);
        }

        public static int ByteArrayToUShort(byte[] bytes, int offset)
        {
            return bytes[offset] | bytes[offset + 1] << 8;
        }

        public static int ByteArrayToUInt(byte[] bytes, int offset)
        {
            return bytes[offset] | bytes[offset + 1] << 8 | bytes[offset + 2] << 16 | bytes[offset + 3] << 24;
        }

        public static byte[] ExtractArray(byte[] bytes, int offset, int length)
        {
            byte[] newArray = new byte[length];
            System.Array.Copy(bytes, offset, newArray, 0, length);
            return newArray;
        }
    }
}
