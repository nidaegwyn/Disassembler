namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;

    public class Push2 : Opcode
    {
        public Push2()
        {
            Name = "PUSH";

            Pattern = new List<BytePattern>()
            {
                new BytePattern(){ ByteValue = 0x50, ByteMask = 0xF8, ApplyMask = true, Variables = { new Tuple<string, int, int>("reg", 5, 3) } },
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
