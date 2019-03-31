namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public abstract class Opcode
    {
        public string Name { get; protected set; }
        public IReadOnlyList<BytePattern> Pattern { get; protected set; }

        public virtual Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = new Instruction();
            i.Name = Name;
            i.Offset = variables["offset"];
            i.Length = variables["length"];

            return i;
        }

        protected Operand GetModRMOperand(Dictionary<string, int> variables)
        {
            Operand rmOp = null;

            // assume wide = true if variable 'w' is missing
            bool wide = !variables.ContainsKey("w") || variables["w"] == 1;
            int mod = variables["mod"];
            int rm = variables["r/m"];

            switch (mod)
            {
                case 0: rmOp = (rm != 6 ? new MemoryAddress(rm, 0, wide) : new MemoryAddress(variables["disp0"] | variables["disp1"] << 8, wide)); break;
                case 1: rmOp = new MemoryAddress(rm, (sbyte)(variables["disp0"]), wide); break;
                case 2: rmOp = new MemoryAddress(rm, variables["disp0"] | variables["disp1"] << 8, wide); break;
                case 3: rmOp = Operands.GetRegister(rm, wide); break;
            }

            return rmOp;
        }

        public bool TryParseData(byte[] data, int baseOffset, out Dictionary<string, int> variables)
        {
            variables = new Dictionary<string, int>();

            int offset = 0;

            bool scanPrefixes = true;
            while (scanPrefixes)
            {
                byte opByte = data[baseOffset + offset];
                switch (opByte)
                {
                    case 0xF0:
                    {
                        variables["lock_prefix"] = 1;
                        offset++;
                    }
                    break;
                    case 0xF2:
                    case 0xF3:
                    {
                        variables["rep_prefix"] = 1;
                        variables["rep_type"] = opByte & 1;
                        offset++;
                    }
                    break;
                    case 0x26:
                    case 0x2E:
                    case 0x36:
                    case 0x3E:
                    {
                        variables["seg_override"] = (opByte >> 3) & 3;
                        offset++;
                    }
                    break;
                    default:
                    {
                        scanPrefixes = false;
                    }
                    break;
                }
            }

            for (int i = 0; i < Pattern.Count; i++)
            {
                if (!Pattern[i].Optional || Pattern[i].Conditional(variables))
                {
                    if ((baseOffset + offset < data.Length) && 
                        (!Pattern[i].ApplyMask || (((Pattern[i].ByteValue ^ data[baseOffset + offset]) & Pattern[i].ByteMask) == 0)))
                    {
                        foreach (Tuple<string, int, int> v in Pattern[i].Variables)
                        {
                            int value = data[baseOffset + offset];
                            value >>= (8 - (v.Item2 + v.Item3));
                            value &= (1 << (v.Item3)) - 1;
                            variables.Add(v.Item1, value);
                        }

                        offset++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            variables["offset"] = baseOffset;
            variables["length"] = offset;

            return true;
        }
    }
}
