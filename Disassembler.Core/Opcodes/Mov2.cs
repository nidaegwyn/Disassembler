namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Mov2 : Opcode
    {
        public Mov2()
        {
            Name = "MOV";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xC6, ByteMask = 0xFE, ApplyMask = true, Variables = { new Tuple<string, int, int>("w", 7, 1) } },
                new BytePattern(){ ByteValue = 0x00, ByteMask = 0x38, ApplyMask = true, Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data0", 0, 8) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("data1", 0, 8) }, Optional = true, Conditional = (d) => d["w"] == 1 },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            bool wide = variables["w"] == 1;

            Operand rmOp = GetModRMOperand(variables);

            Operand dataOp;

            if (wide)
            {
                dataOp = new Immediate16(variables["data0"] | variables["data1"] << 8);
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
