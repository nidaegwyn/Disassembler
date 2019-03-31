namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Lodsb : Opcode
    {
        public Lodsb()
        {
            Name = "LODSB";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xAC, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
