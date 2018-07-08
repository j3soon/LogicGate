using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for GateUI.xaml
    /// </summary>
    public partial class GateUI : UserControl, INotifyPropertyChanged
    {
        /* DependencyProperty */
        public static readonly DependencyProperty GateProperty = DependencyProperty.Register("Gate", typeof(Gate), typeof(GateUI), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnChanged)));
        public Gate Gate
        {
            get { return (Gate)GetValue(GateProperty); }
            set
            {
                SetValue(GateProperty, value);
            }
        }
        public static readonly DependencyProperty StageProperty = DependencyProperty.Register("Stage", typeof(Stage), typeof(GateUI), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnChanged)));
        public Stage Stage
        {
            get { return (Stage)GetValue(StageProperty); }
            set
            {
                SetValue(StageProperty, value);
                UpdateGateItems();
            }
        }

        public ObservableCollection<GateItem> _gateItems;
        public ObservableCollection<GateItem> GateItems
        {
            get { return _gateItems; }
            set
            {
                _gateItems = value;
                RaisePropertyChanged("GateItems");
            }
        }
        public int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
                if (Gate != null)
                {
                    Gate.Type = GateItems[_selectedIndex].Type;
                }
                MainWindow.Check();
            }
        }

        private string _in1Signal;
        public String In1Signal
        {
            get { return _in1Signal; }
            set
            {
                _in1Signal = value;
                RaisePropertyChanged("In1Signal");
            }
        }
        private string _in2Signal;
        public String In2Signal
        {
            get { return _in1Signal; }
            set
            {
                _in1Signal = value;
                RaisePropertyChanged("In2Signal");
            }
        }
        private string _outSignal;
        public String OutSignal
        {
            get { return _outSignal; }
            set
            {
                _outSignal = value;
                RaisePropertyChanged("OutSignal");
            }
        }

        public ImageSource SelectedImage
        {
            get
            {
                if (GateItems != null)
                    return GateItems[SelectedIndex].Image;
                return null;
            }
        }

        public static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GateUI self = (GateUI) d;
            if (e.Property == StageProperty)
                self.UpdateGateItems();
        }

        public void UpdateGateItems()
        {
            GateItem[] gateItems = new GateItem[Stage.Types.Length + 1];
            gateItems[0] = new GateItem(Images.Instance.GateImages[GateTypes.EMPTY], GateTypes.EMPTY);
            for (int i = 0; i < gateItems.Length - 1; i++)
                gateItems[i + 1] = new GateItem(Images.Instance.GateImages[Stage.Types[i]], Stage.Types[i]);
            GateItems = new ObservableCollection<GateItem>(gateItems);
            SelectedIndex = 0;
        }

        public GateUI()
        {
            InitializeComponent();
            //DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //Stage = Program.Stages[1];
            Gate.SignalChanged += Gate_SignalChanged;
        }

        private void Gate_SignalChanged(bool? input1, bool? input2, bool? output)
        {
            if (input1 == null)
                In1Signal = "~";
            else if (input1 == true)
                In1Signal = "1";
            else
                In1Signal = "0";
            if (input2 == null)
                In2Signal = "~";
            else if (input2 == true)
                In2Signal = "1";
            else
                In2Signal = "0";
            if (output == null)
                OutSignal = "~";
            else if (output == true)
                OutSignal = "1";
            else
                OutSignal = "0";
        }
    }
}
