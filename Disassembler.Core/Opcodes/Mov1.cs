namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Mov1 : Opcode
    {
        public Mov1()
        {
            Name = "MOV";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x88, ByteMask = 0xFC, ApplyMask = true, Variables = { new Tuple<string, int, int>("d", 6, 1), new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("reg", 2, 3), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;
            bool from = variables["d"] == 0;
            int reg = variables["reg"];

            Operand regOp = Operands.GetRegister(reg, wide);
            Operand rmOp = GetModRMOperand(variables);

            if (from)
            {
                i.Operands.Add(rmOp);
                i.Operands.Add(regOp);
            }
            else
            {
                i.Operands.Add(regOp);
                i.Operands.Add(rmOp);
            }

            return i;
        }
    }
}
