namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Wait : Opcode
    {
        public Wait()
        {
            Name = "WAIT";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x9B, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
