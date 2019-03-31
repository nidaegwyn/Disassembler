namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Xlat : Opcode
    {
        public Xlat()
        {
            Name = "XLAT";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xD7, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
