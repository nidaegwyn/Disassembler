namespace Disassembler
{
    using Disassembler.Win16;
    using Disassembler.Win16.Resources;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please specify a 16 bit executable as a parameter.");
                Console.WriteLine("For example:");
                Console.WriteLine("Disassembler \"test.exe\"");
                Console.ReadKey();
                return;
            }

            try
            {
                if (!File.Exists(args[0]))
                {
                    Console.WriteLine("Cannot find file {0}", args[0]);
                    Console.ReadKey();
                    return;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot find file {0}", args[0]);
                Console.ReadKey();
                return;
            }

            // Load a 16 bit executable
            Win16Executable exe = Win16Executable.Load(args[0]);


            // Display some header information
            Console.WriteLine("Expected Windows Version {0}.{1}", exe.NewHeader.ExpectedWindowsVersionMajor, exe.NewHeader.ExpectedWindowsVersionMinor);

            for (int i = 0; i < exe.SegmentTable.Length; i++)
            {
                Console.WriteLine("Segment No {0}: Offset: {1} Length: {2} Flags: {3} MinSize: {4}", i + 1, exe.SegmentTable[i].Offset << exe.NewHeader.SectorAlignment, exe.SegmentTable[i].Length, exe.SegmentTable[i].Flags, exe.SegmentTable[i].MinAllocationSize);
            }

            Console.WriteLine("\nResident Names");
            for (int i = 0; i < exe.ResidentNames.Length; i++)
            {
                Console.WriteLine("Name: {0} Ordinal: {1}", exe.ResidentNames[i].Name, exe.ResidentNames[i].Ordinal);
            }

            Console.WriteLine("\nModule Handles");
            for (int i = 0; i < exe.ModuleHandles.Length; i++)
            {
                Console.WriteLine("Name: {0}", exe.ModuleHandles[i]);
            }

            Console.WriteLine("\nImport Name Table");
            for (int i = 0; i < exe.ImportNames.Length; i++)
            {
                Console.WriteLine(exe.ImportNames[i]);
            }

            Console.WriteLine("\nNonResident Names");
            for (int i = 0; i < exe.NonResidentNames.Length; i++)
            {
                Console.WriteLine("Name: {0} Ordinal: {1}", exe.NonResidentNames[i].Name, exe.NonResidentNames[i].Ordinal);
            }


            // Dump disassembled segments into files.
            // TODO: Write class to handle this cleanly
            // CAVEATS:
            //     Does not handle opcode overrides yet
            //     Does not handle jump tables properly
            //     No symbolic labelling of addresses/data yet
            //     No detection of functions
            //     Does not handle segment relocations
            // FUTURE:
            //     Refactor this into trees of BasicBlocks
            //
            for (int i = 0; i < exe.Segments.Length; i++)
            {
                List<string> lines = new List<string>();
                List<Core.Instruction> instrList = Core.Opcodes.Opcodes.ParseData(exe.Segments[i].Data, 0);
                StringBuilder instrBytes = new StringBuilder();
                foreach (Core.Instruction instr in instrList)
                {
                    for (int offset = 0; offset < instr.Length; offset++)
                    {
                        instrBytes.AppendFormat("{0:X2}", exe.Segments[i].Data[instr.Offset + offset]);
                    }
                    lines.Add(string.Format(".{0:X4}{1:X4}: {2,-32} {3}", i + 1, instr.Offset, instrBytes, instr));
                    instrBytes.Clear();
                }
                File.WriteAllLines(string.Format("segment{0}.txt", i + 1), lines);
            }

            for (int i = 0; i < exe.Segments.Length; i++)
            {
                List<string> lines = DumpHex(exe.Segments[i].Data, i + 1);
                File.WriteAllLines(string.Format("segment{0}_hex.txt", i + 1), lines);
            }

            // Write the resource file the executable
            Directory.CreateDirectory("resources");
            ResourceScriptWriter scriptWriter = new ResourceScriptWriter(exe, "resources");
            scriptWriter.WriteScript();

            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
        }

        public static List<string> DumpHex(byte[] data, int segmentNum)
        {
            List<string> lines = new List<string>();
            StringBuilder builder = new StringBuilder();

            for (int offset = 0; offset < data.Length; offset += 16)
            {
                builder.AppendFormat(".{0:X4}{1:X4}: ", segmentNum, offset);
                for (int i = 0; i < 16 && (i + offset) < data.Length; i++)
                {
                    builder.AppendFormat(" {0:X2}", data[offset + i]);
                }
                lines.Add(builder.ToString());
                builder.Clear();
            }

            return lines;
        }
    }
}
