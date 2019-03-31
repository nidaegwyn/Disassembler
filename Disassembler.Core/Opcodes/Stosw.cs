namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Stosw : Opcode
    {
        public Stosw()
        {
            Name = "STOSW";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xAB, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
