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
    public class Gate
    {
        public Gate[] InGates = new Gate[2];
        public Gate OutGate;
        public GateTypes Type;
        public String Id;

        public delegate void SignalChangedEventHandler(bool? input1, bool? input2, bool? output);
        public event SignalChangedEventHandler SignalChanged;

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

        public virtual bool GetOutput()
        {
            throw new NotImplementedException();
        }

        public virtual bool? _getOutput()
        {
            return GetOutput();
        }

        public void ResetSignal()
        {
            SignalChanged?.Invoke(null, null, null);
        }

        protected void UpdateSignal(bool? input1, bool? input2, bool? output)
        {
            SignalChanged?.Invoke(input1, input2, output);
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
        private bool updating = false;

        public EmptyGate(String type)
        {
            Type = (GateTypes)Enum.Parse(typeof(GateTypes), type, true);
        }

        public override bool GetOutput()
        {
            bool? b = _getOutput();
            if (b == null)
                throw new NotImplementedException();
            return (bool) b;
        }
        public override bool? _getOutput()
        {
            bool? In1n, In2n = null;
            In1n = InGates[0]._getOutput();
            if (Type != GateTypes.NOT)
                In2n = InGates[1]._getOutput();
            if (!updating && Type != GateTypes.NOT)
            {
                updating = true;
                bool? b = _getOutput();
                UpdateSignal(In1n, In2n, b);
                updating = false;
                return b;
            }
            if (In1n == null || (Type != GateTypes.NOT && In2n == null))
                return null;
            bool In1 = (bool)In1n;
            bool In2 = false;
            if (Type != GateTypes.NOT)
                In2 = (bool)In2n;
            switch (Type)
            {
                case GateTypes.NOT:
                    return !In1;
                case GateTypes.AND:
                    return In1 && In2;
                case GateTypes.OR:
                    return In1 || In2;
                case GateTypes.XOR:
                    return !In1 && In2 || In1 && !In2;
                case GateTypes.NAND:
                    return !(In1 && In2);
                case GateTypes.NOR:
                    return !(In1 || In2);
                case GateTypes.SWITCH1:
                    return In1;
                case GateTypes.SWITCH2:
                    return In2;
            }
            return null;
        }
    }
}
