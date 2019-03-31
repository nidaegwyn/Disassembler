namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Int1 : Opcode
    {
        public Int1()
        {
            Name = "INT";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xCD, ByteMask = 0xFF, ApplyMask = true },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("type", 0, 8) } }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            Operand dataOp = new Immediate8(variables["type"]);
            i.Operands.Add(dataOp);
            return i;
        }
    }
}
