namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Popf : Opcode
    {
        public Popf()
        {
            Name = "POPF";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x9D, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
