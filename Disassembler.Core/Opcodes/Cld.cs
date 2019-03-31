namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Cld : Opcode
    {
        public Cld()
        {
            Name = "CLD";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xFC, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
