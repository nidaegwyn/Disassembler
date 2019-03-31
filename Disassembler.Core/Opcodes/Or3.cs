namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Or3 : Opcode
    {
        public Or3()
        {
            Name = "OR";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x0C, ByteMask = 0xFE, ApplyMask = true, Variables = { new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data1", 0, 8) }, Optional = true, Conditional = (d) => d["w"] == 1 },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;

            Operand accOp = Operands.GetRegister(0, wide);
            Operand dataOp;

            if (wide)
            {
                dataOp = new Immediate16(variables["data0"] | variables["data1"] << 8);
            }
            else
            {
                dataOp = new Immediate8(variables["data0"]);
            }

            i.Operands.Add(accOp);
            i.Operands.Add(dataOp);

            return i;
        }
    }
}
