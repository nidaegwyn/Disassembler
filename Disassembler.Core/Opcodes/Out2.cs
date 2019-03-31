namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Out2 : Opcode
    {
        public Out2()
        {
            Name = "OUT";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xEE, ByteMask = 0xFE, ApplyMask = true, Variables = { new Tuple<string, int, int>("w", 7, 1) } }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;

            i.Operands.Add(Operands.GetRegister(0, wide));
            i.Operands.Add(Operands.GetRegister16(2));  // DX

            return i;
        }
    }
}
