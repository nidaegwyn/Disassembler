namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Das : Opcode
    {
        public Das()
        {
            Name = "DAS";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x2F, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
