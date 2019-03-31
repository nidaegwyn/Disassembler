namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Pop2 : Opcode
    {
        public Pop2()
        {
            Name = "POP";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x58, ByteMask = 0xF8, ApplyMask = true, Variables = { new Tuple<string, int, int>("reg", 5, 3) } },
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
