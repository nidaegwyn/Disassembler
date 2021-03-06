﻿namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class In1 : Opcode
    {
        public In1()
        {
            Name = "IN";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xE4, ByteMask = 0xFE, ApplyMask = true, Variables = { new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("port", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;

            i.Operands.Add(Operands.GetRegister(0, wide));
            i.Operands.Add(new Immediate8(variables["port"]));

            return i;
        }
    }
}
