namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Movsw : Opcode
    {
        public Movsw()
        {
            Name = "MOVSW";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xA5, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
