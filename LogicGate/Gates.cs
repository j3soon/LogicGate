using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicGate
{
    public enum GateTypes
    {
        EMPTY,
        NOT,
        AND,
        OR,
        NAND,
        NOR,
        XOR,
        SWITCH1,
        SWITCH2,
        TRUE,
        FALSE
    }
    public abstract class Gate
    {
        public Gate[] InGates = new Gate[2];
        public Gate OutGate;
        public abstract bool GetOutput();
        public GateTypes Type;
        public String Id;

        protected Gate()
        {
            InGates[0] = null;
            InGates[1] = null;
            OutGate = null;
        }
        protected Gate(Gate inGate1, Gate inGate2, Gate outGate)
        {
            InGates[0] = inGate1;
            InGates[1] = inGate2;
            OutGate = outGate;
        }
    }

    public class Source : Gate
    {
        public bool inVal;

        public Source(bool inVal)
        {
            this.inVal = inVal;
            Type = (GateTypes)Enum.Parse(typeof(GateTypes), inVal.ToString(), true);
        }
        public override bool GetOutput()
        {
            return inVal;
        }
    }

    public class Dest : Gate
    {
        public bool outVal;

        public Dest(bool outVal)
        {
            this.outVal = outVal;
            Type = (GateTypes)Enum.Parse(typeof(GateTypes), outVal.ToString(), true);
        }
        public override bool GetOutput()
        {
            return InGates[0].GetOutput();
        }
    }

    public class EmptyGate : Gate
    {
        public EmptyGate(String type)
        {
            Type = (GateTypes)Enum.Parse(typeof(GateTypes), type, true);
        }
        public override bool GetOutput()
        {
            switch (Type)
            {
                case GateTypes.NOT:
                    return !InGates[0].GetOutput();
                case GateTypes.AND:
                    return InGates[0].GetOutput() && InGates[1].GetOutput();
                case GateTypes.OR:
                    return InGates[0].GetOutput() || InGates[1].GetOutput();
                case GateTypes.XOR:
                    return !InGates[0].GetOutput() && InGates[1].GetOutput() || InGates[0].GetOutput() && !InGates[1].GetOutput();
                case GateTypes.NAND:
                    return !(InGates[0].GetOutput() && InGates[1].GetOutput());
                case GateTypes.NOR:
                    return !(InGates[0].GetOutput() || InGates[1].GetOutput());
                case GateTypes.SWITCH1:
                    return InGates[0].GetOutput();
                case GateTypes.SWITCH2:
                    return InGates[1].GetOutput();
            }
            throw new NotImplementedException();
        }
    }
}
