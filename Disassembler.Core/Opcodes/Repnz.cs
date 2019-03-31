namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Repnz : Opcode
    {
        public Repnz()
        {
            Name = "REPNZ";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xF2, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
