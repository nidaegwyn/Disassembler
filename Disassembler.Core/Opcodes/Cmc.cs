namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Cmc : Opcode
    {
        public Cmc()
        {
            Name = "CMC";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xF5, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
