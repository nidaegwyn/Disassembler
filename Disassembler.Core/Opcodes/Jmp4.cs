namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Jmp4 : Opcode
    {
        public Jmp4()
        {
            Name = "JMP FAR";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xEA, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("off0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("off1", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("seg0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("seg1", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(new SegmentedMemoryAddress(variables["seg0"] | variables["seg1"] << 8, variables["off0"] | variables["off1"] << 8));
            return i;
        }
    }
}
