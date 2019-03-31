namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Les : Opcode
    {
        public Les()
        {
            Name = "LES";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xC4, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("reg", 2, 3), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            i.Operands.Add(Operands.GetRegister16(variables["reg"]));
            i.Operands.Add(GetModRMOperand(variables));

            return i;
        }
    }
}
