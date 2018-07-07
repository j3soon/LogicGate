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
                    Gate.Type = GateItems[_selectedIndex].Type;
                MainWindow.Check();
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
        }
    }
}
