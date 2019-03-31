﻿namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Stosb : Opcode
    {
        public Stosb()
        {
            Name = "STOSB";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xAA, ByteMask = 0xFF, ApplyMask = true },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            return i;
        }
    }
}
