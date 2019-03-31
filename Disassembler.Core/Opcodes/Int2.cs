namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Int2 : Opcode
    {
        public Int2()
        {
            Name = "INT";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xCC, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(new Immediate8(3));
            return i;
        }
    }
}
