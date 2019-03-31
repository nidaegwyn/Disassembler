namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Xchg2 : Opcode
    {
        public Xchg2()
        {
            Name = "XCHG";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x90, ByteMask = 0xF8, ApplyMask = true, Variables = { new Tuple<string, int, int>("reg", 5, 3) } }
            }.AsReadOnly();
        }

        public override Instruction GetInstruction(Dictionary<string, int> variables)
        {
            Instruction i = base.GetInstruction(variables);

            i.Operands.Add(Operands.GetRegister16(0));
            i.Operands.Add(Operands.GetRegister16(variables["reg"]));

            return i;
        }
    }
}
