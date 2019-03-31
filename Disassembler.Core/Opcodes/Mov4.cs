namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Mov4 : Opcode
    {
        public Mov4()
        {
            Name = "MOV";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xA0, ByteMask = 0xFC, ApplyMask = true, Variables = { new Tuple<string, int, int>("d", 6, 1), new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("addr0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("addr1", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;
            bool from = variables["d"] == 0;

            Operand regOp = Operands.GetRegister(0, wide);
            Operand memOp = new MemoryAddress(variables["addr0"] | variables["addr1"] << 8, wide);

            if (from)
            {
                i.Operands.Add(regOp);
                i.Operands.Add(memOp);
            }
            else
            {
                i.Operands.Add(memOp);
                i.Operands.Add(regOp);
            }

            return i;
        }
    }
}
