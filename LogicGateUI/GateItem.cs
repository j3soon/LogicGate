using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LogicGate;

namespace LogicGateUI
{
    public class GateItem
    {
        public ImageSource Image { get; set; }
        public GateTypes Type { get; set; }

        public GateItem(ImageSource image, GateTypes type)
        {
            Image = image;
            Type = type;
        }
    }
}
