namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Aas : Opcode
    {
        public Aas()
        {
            Name = "AAS";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x3F, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
