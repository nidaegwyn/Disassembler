namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class SegOverride : Opcode
    {
        public SegOverride()
        {
            Name = "SEG";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x26, ByteMask = 0xE7, ApplyMask = true, Variables = { new Tuple<string, int, int>("reg", 3, 2) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            i.Operands.Add(Operands.GetSegmentRegister(variables["reg"]));

            return i;
        }
    }
}
