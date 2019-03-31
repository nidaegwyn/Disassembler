﻿namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Movsb : Opcode
    {
        public Movsb()
        {
            Name = "MOVSB";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xA4, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
