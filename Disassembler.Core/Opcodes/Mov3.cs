namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Mov3 : Opcode
    {
        public Mov3()
        {
            Name = "MOV";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xB0, ByteMask = 0xF0, ApplyMask = true, Variables = { new Tuple<string, int, int>("w", 4, 1), new Tuple<string, int, int>("reg", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data1", 0, 8) }, Optional = true, Conditional = (d) => d["w"] == 1 },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool wide = variables["w"] == 1;
            int reg = variables["reg"];

            Operand regOp = Operands.GetRegister(reg, wide);
            Operand dataOp;

            if (wide)
            {
                dataOp = new Immediate16(variables["data0"] | variables["data1"] << 8);
            }
            else
            {
                dataOp = new Immediate8(variables["data0"]);
            }

            i.Operands.Add(regOp);
            i.Operands.Add(dataOp);

            return i;
        }
    }
}
