namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Aad : Opcode
    {
        public Aad()
        {
            Name = "AAD";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xD5, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ ByteValue = 0x0A, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
