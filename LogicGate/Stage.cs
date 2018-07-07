using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicGate
{
    public class Stage
    {
        public int And, Or, S1, S2;
        public GateTypes[] Types { get; set; }

        public Stage(int and, int or, int s1, int s2, GateTypes[] type)
        {
            And = and;
            Or = or;
            S1 = s1;
            S2 = s2;
            Types = type;
        }
    }
}
