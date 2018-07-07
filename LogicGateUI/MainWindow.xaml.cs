using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IStage S1;
        public static void Check()
        {
            S1.Refresh();
        }
        public Stage Stage1 { get; set; } = Program.Stages[1];

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            S1 = s1;
        }
    }
}
