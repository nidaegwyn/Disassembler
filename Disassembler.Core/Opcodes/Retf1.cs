namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Retf1 : Opcode
    {
        public Retf1()
        {
            Name = "RETF";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xCB, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
