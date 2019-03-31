namespace Disassembler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Instruction
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public List<Operand> Operands { get; set; }
        public string Name { get; set; }

        public Instruction()
        {
            this.Operands = new List<Operand>();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, string.Join(", ", Operands));
        }
    }
}
