namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Add1 : Opcode
    {
        public Add1()
        {
            Name = "ADD";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x00, ByteMask = 0xFC, ApplyMask = true, Variables = { new Tuple<string, int, int>("d", 6, 1), new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("reg", 2, 3), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool from = variables["d"] == 0;
            bool wide = variables["w"] == 1;

            Operand regOp = Operands.GetRegister(variables["reg"], wide);
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
