namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Pushf : Opcode
    {
        public Pushf()
        {
            Name = "PUSHF";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x9C, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
