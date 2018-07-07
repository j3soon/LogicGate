using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicGate;

namespace LogicGateUI
{
    /// <summary>
    /// Interaction logic for Stage1.xaml
    /// </summary>
    public partial class Stage1 : UserControl
    {
        public Stage Stage { get; set; }

        public GateTypes[] Types => Stage.Types;

        public Gate g_1_1 { get; set; }
        public Gate g_1_2 { get; set; }
        public Gate g_2_1 { get; set; }
        public Gate g_3_1 { get; set; }

        public Stage1()
        {
            Stage = Program.Stages[1];
            InitializeComponent();
            DataContext = this;
            Program.LoadExternalStage(1);
            g_1_1 = Program.Ids["1-1"];
            g_1_2 = Program.Ids["1-2"];
            g_2_1 = Program.Ids["2-1"];
            g_3_1 = Program.Ids["3-1"];
        }
    }
}
