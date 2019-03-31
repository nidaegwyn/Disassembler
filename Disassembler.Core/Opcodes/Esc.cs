namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Esc : Opcode
    {
        public Esc()
        {
            Name = "ESC";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0xD8, ByteMask = 0xF8, ApplyMask = true, Variables = { new Tuple<string, int, int>("esc", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("mod", 0, 2), new Tuple<string, int, int>("reg", 2, 3), new Tuple<string, int, int>("r/m", 5, 3) } },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp0", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 1 || d["mod"] == 2 },
                new BytePattern(){ Variables = { new Tuple<string, int, int>("disp1", 0, 8) }, Optional = true, Conditional = (d) => (d["mod"] == 0 && d["r/m"] == 6) || d["mod"] == 2 }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(new Immediate8(variables["esc"]));
            return i;
        }
    }
}
