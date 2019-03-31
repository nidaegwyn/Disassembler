namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Jcxz : Opcode
    {
        public Jcxz()
        {
            Name = "JCXZ";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xE3, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            Operand relOffsetOp = new Immediate16((sbyte)(variables["disp0"]) + variables["offset"] + variables["length"]);
            i.Operands.Add(relOffsetOp);
            return i;
        }
    }
}
