namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Repz : Opcode
    {
        public Repz()
        {
            Name = "REPZ";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xF3, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
