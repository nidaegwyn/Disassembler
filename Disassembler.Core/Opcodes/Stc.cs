namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Stc : Opcode
    {
        public Stc()
        {
            Name = "STC";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xF9, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
