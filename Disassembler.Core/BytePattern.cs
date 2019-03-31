namespace Disassembler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BytePattern
    {
        public byte ByteValue { get; set; }
        public byte ByteMask { get; set; }
        public bool Optional { get; set; }
        public bool ApplyMask { get; set; }
        public Func<Dictionary<string, int>, bool> Conditional { get; set; }
        public List<Tuple<string, int, int>> Variables { get; set; }

        public BytePattern()
        {
            this.Variables = new List<Tuple<string, int, int>>();
        }
    }
}
