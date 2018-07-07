using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Stage2.xaml
    /// </summary>
    public partial class Stage3 : UserControl, INotifyPropertyChanged, IStage
    {
        private Stage _stage;
        public Stage Stage
        {
            get { return _stage; }
            set
            {
                _stage = value;
                RaisePropertyChanged("Stage");
            }
        }

        private ObservableDictionary<string, Gate> _gates;
        public ObservableDictionary<String, Gate> Gates
        {
            get { return _gates; }
            set
            {
                _gates = value;
                RaisePropertyChanged("Gates");
            }
        }

        private String _result;

        public String Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
            }
        }

        public Stage3()
        {
            InitializeComponent();
            //DataContext = this;
        }

        private void Stage1_Loaded(object sender, RoutedEventArgs e)
        {
            Result = "X";
            Program.LoadExternalStage(3);
            Stage = Program.Stages[3];
            Gates = new ObservableDictionary<String, Gate>(Program.Ids);
            Refresh();
        }

        public void Refresh()
        {
            try
            {
                if (Program.Check())
                    Result = "O";
                else
                    Result = "X";
            }
            catch (NotImplementedException)
            {
                Result = "~";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
