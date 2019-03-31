namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Cbw : Opcode
    {
        public Cbw()
        {
            Name = "CBW";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x98, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
