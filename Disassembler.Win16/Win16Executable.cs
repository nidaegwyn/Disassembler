namespace Disassembler.Win16
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Win16Executable
    {
        public MZHeader Header { get; private set; }
        public NEHeader NewHeader { get; private set; }
        public SegmentEntry[] SegmentTable { get; private set; }
        public ResourceGroup[] ResourceTable { get; private set; }
        public NameEntry[] ResidentNames { get; private set; }
        public string[] ModuleHandles { get; private set; }
        public string[] ImportNames { get; private set; }
        public NameEntry[] NonResidentNames { get; private set; }
        public Segment[] Segments { get; private set; }
        public Entry[] EntryPoints { get; private set; }

        public static Win16Executable Load(string filename)
        {
            Win16Executable exe = new Win16Executable();

            byte[] bytes = File.ReadAllBytes(filename);

            exe.Header = ByteArrayUtils.ByteArrayToStructure<MZHeader>(bytes, 0);
            exe.NewHeader = ByteArrayUtils.ByteArrayToStructure<NEHeader>(bytes, exe.Header.NewHeaderOffset);
            exe.SegmentTable = ByteArrayUtils.ByteArrayToStructureArray<SegmentEntry>(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.SegmentTableOffset, exe.NewHeader.NumSegments);
            exe.ResourceTable = LoadResourceTable(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.ResourceTableOffset, exe.NewHeader.ResidentNameTableOffset - exe.NewHeader.ResourceTableOffset);
            exe.ResidentNames = LoadNameTable(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.ResidentNameTableOffset);
            exe.ModuleHandles = LoadModuleHandles(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.ModuleTableOffset, exe.NewHeader.NumModules, exe.Header.NewHeaderOffset + exe.NewHeader.ImportNameTableOffset);
            exe.ImportNames = LoadImportNames(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.ImportNameTableOffset, exe.Header.NewHeaderOffset + exe.NewHeader.EntryTableOffset);
            exe.NonResidentNames = LoadNameTable(bytes, (int)exe.NewHeader.NonResidentNameTableOffset);
            exe.Segments = LoadSegments(bytes, exe.NewHeader.NumSegments, exe.SegmentTable, exe.NewHeader.SectorAlignment);
            exe.EntryPoints = LoadEntryTable(bytes, exe.Header.NewHeaderOffset + exe.NewHeader.EntryTableOffset);

            return exe;
        }

        private static ResourceGroup[] LoadResourceTable(byte[] bytes, int offset, int length)
        {
            int currentOffset = 0;
            List<ResourceGroup> resources = new List<ResourceGroup>();

            if (currentOffset < length)
            {
                int resourceAlignment = ByteArrayUtils.ByteArrayToUShort(bytes, offset);
                currentOffset += 2;

                while (ByteArrayUtils.ByteArrayToUShort(bytes, offset + currentOffset) != 0)
                {
                    resources.Add(LoadResourceGroup(bytes, offset, ref currentOffset, resourceAlignment));
                }
                // skip resource names, as they are pulled in to the group/resource.
            }

            return resources.ToArray();
        }

        private static ResourceGroup LoadResourceGroup(byte[] bytes, int baseOffset, ref int currentOffset, int resourceAlignment)
        {
            ResourceGroup resourceGroup = new ResourceGroup();
            resourceGroup.TypeId = ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset);
            currentOffset += 2;

            if ((resourceGroup.TypeId & 0x8000) != 0)
            {
                resourceGroup.ResType = (ResourceType)(resourceGroup.TypeId ^ 0x8000);
            }
            else
            {
                int nameOffset = baseOffset + resourceGroup.TypeId;
                int nameLength = bytes[nameOffset];
                resourceGroup.TypeName = ByteArrayUtils.ByteArrayToString(bytes, nameOffset + 1, nameLength);
            }

            int resourceCount = ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset);
            currentOffset += 6;

            resourceGroup.Resources = new Resource[resourceCount];
            for (int i = 0; i < resourceCount; i++)
            {
                resourceGroup.Resources[i] = LoadResource(bytes, baseOffset, ref currentOffset, resourceAlignment);
            }

            return resourceGroup;
        }

        private static Resource LoadResource(byte[] bytes, int baseOffset, ref int currentOffset, int resourceAlignment)
        {
            Resource resource = new Resource();

            int resourceOffset = ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset) << resourceAlignment;
            currentOffset += 2;
            int resourceLength = ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset) << resourceAlignment;
            currentOffset += 2;
            resource.Data = ByteArrayUtils.ExtractArray(bytes, resourceOffset, resourceLength);

            resource.Flags = (ResourceFlags)ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset);
            currentOffset += 2;
            resource.Id = ByteArrayUtils.ByteArrayToUShort(bytes, baseOffset + currentOffset);
            currentOffset += 6;

            if ((resource.Id & 0x8000) != 0)
            {
                resource.Id ^= 0x8000; // turn off flag bit
                resource.Name = string.Empty;
            }
            else
            {
                int nameOffset = baseOffset + resource.Id;
                int nameLength = bytes[nameOffset];
                resource.Name = ByteArrayUtils.ByteArrayToString(bytes, nameOffset + 1, nameLength);
            }

            return resource;
        }

        private static NameEntry[] LoadNameTable(byte[] bytes, int offset)
        {
            List<NameEntry> names = new List<NameEntry>();
            int currentOffset = offset;
            while (bytes[currentOffset] != 0)
            {
                int length = bytes[currentOffset++];
                NameEntry entry = new NameEntry()
                {
                    Name = ByteArrayUtils.ByteArrayToString(bytes, currentOffset, length),
                    Ordinal = ByteArrayUtils.ByteArrayToUShort(bytes, currentOffset + length),
                };
                names.Add(entry);
                currentOffset += length + 2;
            }

            return names.ToArray();
        }

        private static string[] LoadModuleHandles(byte[] bytes, int moduleTableOffset, int moduleCount, int importTableOffset)
        {
            string[] modules = new string[moduleCount];
            for (int i = 0; i < moduleCount; i++)
            {
                int stringOffset = ByteArrayUtils.ByteArrayToUShort(bytes, moduleTableOffset + i * 2);
                int length = bytes[importTableOffset + stringOffset];
                modules[i] = ByteArrayUtils.ByteArrayToString(bytes, importTableOffset + stringOffset + 1, length);
            }

            return modules;
        }

        private static string[] LoadImportNames(byte[] bytes, int offset, int endOffset)
        {
            List<string> names = new List<string>();
            if (endOffset - offset > 0)
            {
                int currentOffset = offset;
                while (bytes[currentOffset] != 0)
                {
                    int length = bytes[currentOffset++];
                    names.Add(ByteArrayUtils.ByteArrayToString(bytes, currentOffset, length));
                    currentOffset += length;
                }
            }

            return names.ToArray();
        }

        private static Segment[] LoadSegments(byte[] bytes, int numSegments, SegmentEntry[] segmentTable, int sectorAlignment)
        {
            Segment[] segments = new Segment[numSegments];

            for (int i = 0; i < numSegments; i++)
            {
                segments[i].Data = new byte[segmentTable[i].MinAllocationSize];
                Array.Copy(bytes, segmentTable[i].Offset << sectorAlignment, segments[i].Data, 0, segmentTable[i].Length);
                if ((segmentTable[i].Flags & SegmentFlags.RelocInfo) != 0)
                {
                    int relocOffset = (segmentTable[i].Offset << sectorAlignment) + segmentTable[i].Length;
                    int relocCount = ByteArrayUtils.ByteArrayToUShort(bytes, relocOffset);
                    relocOffset += 2;
                    segments[i].Relocations = ByteArrayUtils.ByteArrayToStructureArray<RelocationInfo>(bytes, relocOffset, relocCount);
                }
            }

            return segments;
        }

        private static Entry[] LoadEntryTable(byte[] bytes, int offset)
        {
            List<Entry> entryTable = new List<Entry>();
            entryTable.Add(new Entry { Type = EntryType.Empty });

            int currentOffset = offset;
            while (bytes[currentOffset] != 0)
            {
                int numEntries = bytes[currentOffset++];
                int bundleType = bytes[currentOffset++];
                switch (bundleType)
                {
                    case 0x00: // empty bundle
                    {
                        for (int i = 0; i < numEntries; i++)
                        {
                            entryTable.Add(new Entry() { Type = EntryType.Empty });
                        }
                    }
                    break;
                    case 0xff: // moveable
                    {
                        for (int i = 0; i < numEntries; i++)
                        {
                            Entry entry = new Entry();
                            entry.Type = EntryType.Moveable;
                            entry.Flags = (EntryFlags)bytes[currentOffset++];
                            currentOffset += 2;
                            entry.Segment = bytes[currentOffset++];
                            entry.Offset = (ushort)ByteArrayUtils.ByteArrayToUShort(bytes, currentOffset);
                            currentOffset += 2;
                            entryTable.Add(entry);
                        }
                    }
                    break;
                    case 0xfe: // exported constant
                    {
                        for (int i = 0; i < numEntries; i++)
                        {
                            Entry entry = new Entry();
                            entry.Type = EntryType.ConstantValue;
                            entry.Segment = (byte)bundleType;
                            entry.Flags = (EntryFlags)bytes[currentOffset++];
                            entry.Offset = (ushort)ByteArrayUtils.ByteArrayToUShort(bytes, currentOffset);
                            currentOffset += 2;
                            entryTable.Add(entry);
                        }
                    }
                    break;
                    default:
                    {
                        for (int i = 0; i < numEntries; i++)
                        {
                            Entry entry = new Entry();
                            entry.Type = EntryType.Fixed;
                            entry.Segment = (byte)bundleType;
                            entry.Flags = (EntryFlags)bytes[currentOffset++];
                            entry.Offset = (ushort)ByteArrayUtils.ByteArrayToUShort(bytes, currentOffset);
                            currentOffset += 2;
                            entryTable.Add(entry);
                        }
                    }
                    break;
                }
            }

            return entryTable.ToArray();
        }
    }
}
