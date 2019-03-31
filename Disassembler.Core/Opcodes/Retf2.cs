namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Retf2 : Opcode
    {
        public Retf2()
        {
            Name = "RETF";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xCA, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data1", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(new Immediate16(variables["data0"] | variables["data1"] << 8));
            return i;
        }
    }
}
