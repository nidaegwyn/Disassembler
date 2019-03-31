namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Lahf : Opcode
    {
        public Lahf()
        {
            Name = "LAHF";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x9F, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
