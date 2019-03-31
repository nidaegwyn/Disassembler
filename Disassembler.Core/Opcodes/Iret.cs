namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Iret : Opcode
    {
        public Iret()
        {
            Name = "IRET";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xCF, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
