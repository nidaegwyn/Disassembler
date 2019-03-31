namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Dec2 : Opcode
    {
        public Dec2()
        {
            Name = "DEC";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x48, ByteMask = 0xF8, ApplyMask = true, Variables = { new Tuple<string, int, int>("reg", 5, 3) } },
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);
            i.Operands.Add(Operands.GetRegister16(variables["reg"]));
            return i;
        }
    }
}
