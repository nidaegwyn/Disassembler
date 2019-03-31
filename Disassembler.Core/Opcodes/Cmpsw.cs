namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Cmpsw : Opcode
    {
        public Cmpsw()
        {
            Name = "CMPSW";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xA7, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
