namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Sti : Opcode
    {
        public Sti()
        {
            Name = "STI";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xFB, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
