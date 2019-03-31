namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Daa : Opcode
    {
        public Daa()
        {
            Name = "DAA";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x27, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
