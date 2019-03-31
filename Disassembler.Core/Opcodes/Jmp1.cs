namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Jmp1 : Opcode
    {
        public Jmp1()
        {
            Name = "JMP NEAR";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xE9, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            Operand relOffsetOp = new Immediate16((short)(variables["disp0"] | variables["disp1"] << 8) + variables["offset"] + variables["length"]);
            i.Operands.Add(relOffsetOp);
            return i;
        }
    }
}
