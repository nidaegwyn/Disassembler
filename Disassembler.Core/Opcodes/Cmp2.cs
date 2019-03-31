namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Cmp2 : Opcode
    {
        public Cmp2()
        {
            Name = "CMP";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x80, ByteMask = 0xFC, ApplyMask = true, Variables = { new Tuple<string, int, int>("s", 6, 1), new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ ByteValue = 0x38, ByteMask = 0x38, ApplyMask = true, Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data1", 0, 8) }, Optional = true, Conditional = (d) => d["s"] == 0 && d["w"] == 1 },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            bool signExtend = variables["s"] == 1;
            bool wide = variables["w"] == 1;

            Operand rmOp = GetModRMOperand(variables);
            Operand dataOp;


            if (wide)
            {
                if (!signExtend)
                {
                    dataOp = new Immediate16(variables["data0"] | variables["data1"] << 8);
                }
                else
                {
                    dataOp = new Immediate16((sbyte)variables["data0"]);
                }
            }
            else
            {
                dataOp = new Immediate8(variables["data0"]);
            }

            i.Operands.Add(rmOp);
            i.Operands.Add(dataOp);

            return i;
        }
    }
}
