namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Rcr : Opcode
    {
        public Rcr()
        {
            Name = "RCR";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xD0, ByteMask = 0xFC, ApplyMask = true, Variables = { new Tuple<string, int, int>("v", 6, 1), new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ ByteValue = 0x18, ByteMask = 0x38, ApplyMask = true, Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(GetModRMOperand(variables));

            if (variables["v"] == 0)
            {
                i.Operands.Add(new Immediate8(1));
            }
            else
            {
                i.Operands.Add(Operands.GetRegister8(1)); // CL
            }
            return i;
        }
    }
}
