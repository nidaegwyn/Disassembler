namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Nop : Opcode
    {
        public Nop()
        {
            Name = "NOP";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x90, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
