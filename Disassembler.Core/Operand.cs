namespace Disassembler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Operand
    {
        public int Value { get; private set; }
        public abstract string Name { get; }

        public Operand(int value)
        {
            this.Value = value;
        }
    }

    public class Operands
    {
        private static Operand[] Register8 = 
        {
            new Register8(0),
            new Register8(1),
            new Register8(2),
            new Register8(3),
            new Register8(4),
            new Register8(5),
            new Register8(6),
            new Register8(7),
        };

        private static Operand[] Register16 = 
        {
            new Register16(0),
            new Register16(1),
            new Register16(2),
            new Register16(3),
            new Register16(4),
            new Register16(5),
            new Register16(6),
            new Register16(7),
        };

        private static Operand[] Segments = 
        {
            new Segment(0),
            new Segment(1),
            new Segment(2),
            new Segment(3),
        };

        public static Operand GetRegister8(int value)
        {
            Debug.Assert(value >= 0 && value < 8, "Value must be between 0 and 7");
            return Register8[value];
        }

        public static Operand GetRegister16(int value)
        {
            Debug.Assert(value >= 0 && value < 8, "Value must be between 0 and 7");
            return Register16[value];
        }
        
        public static Operand GetRegister(int value, bool wide)
        {
            if (wide) return GetRegister16(value);
            return GetRegister8(value);
        }

        public static Operand GetSegmentRegister(int value)
        {
            Debug.Assert(value >= 0 && value < 4, "Value must be between 0 and 3");
            return Segments[value];
        }

        public static void DecodeModRegRM(byte data, out int modValue, out int regValue, out int rmValue)
        {
            modValue = (data & 0xc0) >> 6; // two highest bits
            regValue = (data & 0x38) >> 3; // next 3 bits
            rmValue = (data & 0x07); // lowest 3 bits
        }
    }

    public class Register8 : Operand
    {
        private static string[] regNames = 
        {
            "AL", "CL", "DL", "BL",
            "AH", "CH", "DH", "BH",
        };

        public Register8(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return regNames[Value]; }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Register16 : Operand
    {
        private static string[] regNames = 
        {
            "AX", "CX", "DX", "BX",
            "SP", "BP", "SI", "DI",
        };

        public Register16(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return regNames[Value]; }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Segment : Operand
    {
        private static string[] segNames = 
        {
            "ES", "CS", "SS", "DS",
        };

        public Segment(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return segNames[Value]; }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class MemoryAddress : Operand
    {
        private static string[] rmNames =
        {
            "BX+SI", // DS?
            "BX+DI", // DS?
            "BP+SI", // SS?
            "BP+DI", // SS?
            "SI", // DS
            "DI", // DS
            "BP", // SS
            "BX", // DS
        };

        public int RM { get; private set; }
        public bool Wide { get; private set; }

        public MemoryAddress(int rm, int value, bool wide)
            : base(value)
        {
            RM = rm;
            Wide = wide;
        }

        public MemoryAddress(int value, bool wide)
            : base(value)
        {
            RM = -1;
            Wide = wide;
        }

        public override string Name
        {
            get
            {
                if (RM == -1) return "" + string.Format("{0:X4}h", Value);
                return rmNames[RM] + (Value < 0 ? "-" + string.Format("{0:X4}h", -Value) : (Value > 0 ? "+" + string.Format("{0:X4}h", Value) : ""));
            }
        }

        public override string ToString()
        {
            return ((Wide) ? "WORD" : "BYTE") + " PTR [" + Name + "]";
        }
    }

    public class Immediate8 : Operand
    {
        public Immediate8(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return string.Format("{0:X2}h", Value); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Immediate16 : Operand
    {
        public Immediate16(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return string.Format("{0:X4}h", Value); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SegmentedMemoryAddress : Operand
    {
        public SegmentedMemoryAddress(int seg, int offset)
            : base(seg << 16 | offset)
        {
        }

        public override string Name
        {
            get { return string.Format("{0:X4}h:{1:X4}h", (Value >> 16) & 0xFFFF, Value & 0xFFFF); }
        }

        public override string ToString()
        {
            return "FAR PTR [" + Name + "]";
        }
    }

    public class NearPtr : Operand
    {
        public NearPtr(int value)
            : base(value)
        {
        }

        public override string Name
        {
            get { return string.Format("{0:X4}h", Value & 0xFFFF); }
        }

        public override string ToString()
        {
            return "NEAR PTR [" + Name + "]";
        }
    }
}
