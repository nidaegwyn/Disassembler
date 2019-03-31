namespace Disassembler.Core.Opcodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Opcodes
    {
        private static List<Opcode> opcodeList;

        static Opcodes()
        {
            opcodeList = new List<Opcode>();
            opcodeList.Add(new Aaa());
            opcodeList.Add(new Aad());
            opcodeList.Add(new Aam());
            opcodeList.Add(new Aas());
            opcodeList.Add(new Adc1());
            opcodeList.Add(new Adc2());
            opcodeList.Add(new Adc3());
            opcodeList.Add(new Add1());
            opcodeList.Add(new Add2());
            opcodeList.Add(new Add3());
            opcodeList.Add(new And1());
            opcodeList.Add(new And2());
            opcodeList.Add(new And3());
            opcodeList.Add(new Call1());
            opcodeList.Add(new Call2());
            opcodeList.Add(new Call3());
            opcodeList.Add(new Call4());
            opcodeList.Add(new Cbw());
            opcodeList.Add(new Clc());
            opcodeList.Add(new Cld());
            opcodeList.Add(new Cli());
            opcodeList.Add(new Cmc());
            opcodeList.Add(new Cmp1());
            opcodeList.Add(new Cmp2());
            opcodeList.Add(new Cmp3());
            opcodeList.Add(new Cmpsb());
            opcodeList.Add(new Cmpsw());
            opcodeList.Add(new Cwd());
            opcodeList.Add(new Daa());
            opcodeList.Add(new Das());
            opcodeList.Add(new Dec1());
            opcodeList.Add(new Dec2());
            opcodeList.Add(new Div());
            opcodeList.Add(new Esc());
            opcodeList.Add(new Hlt());
            opcodeList.Add(new Idiv());
            opcodeList.Add(new Imul());
            opcodeList.Add(new In1());
            opcodeList.Add(new In2());
            opcodeList.Add(new Inc1());
            opcodeList.Add(new Inc2());
            opcodeList.Add(new Int1());
            opcodeList.Add(new Int2());
            opcodeList.Add(new Into());
            opcodeList.Add(new Iret());
            opcodeList.Add(new Jb());
            opcodeList.Add(new Jbe());
            opcodeList.Add(new Jcxz());
            opcodeList.Add(new Je());
            opcodeList.Add(new Jl());
            opcodeList.Add(new Jle());
            opcodeList.Add(new Jmp1());
            opcodeList.Add(new Jmp2());
            opcodeList.Add(new Jmp3());
            opcodeList.Add(new Jmp4());
            opcodeList.Add(new Jmp5());
            opcodeList.Add(new Jnb());
            opcodeList.Add(new Jnbe());
            opcodeList.Add(new Jne());
            opcodeList.Add(new Jnl());
            opcodeList.Add(new Jnle());
            opcodeList.Add(new Jno());
            opcodeList.Add(new Jnp());
            opcodeList.Add(new Jns());
            opcodeList.Add(new Jo());
            opcodeList.Add(new Jp());
            opcodeList.Add(new Js());
            opcodeList.Add(new Lahf());
            opcodeList.Add(new Lds());
            opcodeList.Add(new Lea());
            opcodeList.Add(new Les());
            opcodeList.Add(new Lock());
            opcodeList.Add(new Lodsb());
            opcodeList.Add(new Lodsw());
            opcodeList.Add(new Loop());
            opcodeList.Add(new Loopnz());
            opcodeList.Add(new Loopz());
            opcodeList.Add(new Mov1());
            opcodeList.Add(new Mov2());
            opcodeList.Add(new Mov3());
            opcodeList.Add(new Mov4());
            opcodeList.Add(new Mov5());
            opcodeList.Add(new Movsb());
            opcodeList.Add(new Movsw());
            opcodeList.Add(new Mul());
            opcodeList.Add(new Neg());
            opcodeList.Add(new Nop());
            opcodeList.Add(new Not());
            opcodeList.Add(new Or1());
            opcodeList.Add(new Or2());
            opcodeList.Add(new Or3());
            opcodeList.Add(new Out1());
            opcodeList.Add(new Out2());
            opcodeList.Add(new Pop1());
            opcodeList.Add(new Pop2());
            opcodeList.Add(new Pop3());
            opcodeList.Add(new Popf());
            opcodeList.Add(new Push1());
            opcodeList.Add(new Push2());
            opcodeList.Add(new Push3());
            opcodeList.Add(new Pushf());
            opcodeList.Add(new Rcl());
            opcodeList.Add(new Rcr());
            opcodeList.Add(new Repnz());
            opcodeList.Add(new Repz());
            opcodeList.Add(new Ret1());
            opcodeList.Add(new Ret2());
            opcodeList.Add(new Retf1());
            opcodeList.Add(new Retf2());
            opcodeList.Add(new Rol());
            opcodeList.Add(new Ror());
            opcodeList.Add(new Sahf());
            opcodeList.Add(new Sar());
            opcodeList.Add(new Sbb1());
            opcodeList.Add(new Sbb2());
            opcodeList.Add(new Sbb3());
            opcodeList.Add(new Scasb());
            opcodeList.Add(new Scasw());
            opcodeList.Add(new Shl());
            opcodeList.Add(new Shr());
            opcodeList.Add(new Stc());
            opcodeList.Add(new Std());
            opcodeList.Add(new Sti());
            opcodeList.Add(new Stosb());
            opcodeList.Add(new Stosw());
            opcodeList.Add(new Sub1());
            opcodeList.Add(new Sub2());
            opcodeList.Add(new Sub3());
            opcodeList.Add(new Test1());
            opcodeList.Add(new Test2());
            opcodeList.Add(new Test3());
            opcodeList.Add(new Wait());
            opcodeList.Add(new Xchg1());
            opcodeList.Add(new Xchg2());
            opcodeList.Add(new Xlat());
            opcodeList.Add(new Xor1());
            opcodeList.Add(new Xor2());
            opcodeList.Add(new Xor3());
            opcodeList.Add(new SegOverride());
        }

        public static List<Instruction> ParseData(byte[] data, int baseOffset)
        {
            List<Instruction> instructions = new List<Instruction>();

            int offset = 0;

            while (baseOffset < data.Length)
            {
                Dictionary<string, int> variables;

                foreach (Opcode opcode in opcodeList)
                {
                    if (opcode.TryParseData(data, baseOffset, out variables))
                    {
                        instructions.Add(opcode.GetInstruction(variables));
                        offset = variables["length"];
                        break;
                    }
                }

                if (offset == 0)
                {
                    //Console.WriteLine("No opcode matches 0x{0:x2}", data[baseOffset]);
                    instructions.Add(new Instruction() { Name = "DB", Offset = baseOffset, Length = 1, Operands = { new Immediate8(data[baseOffset]) } });
                    offset++; // skip byte
                }

                baseOffset += offset;
                offset = 0;

                //foreach (string name in variables.Keys)
                //{
                //    Console.WriteLine("{0} = {1}", name, variables[name]);
                //}
                //Console.WriteLine();
            }

            return instructions;
        }
    }
}
