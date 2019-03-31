namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Ret1 : Opcode
    {
        public Ret1()
        {
            Name = "RET";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xC3, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
