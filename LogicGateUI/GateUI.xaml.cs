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
    /// Interaction logic for GateUI.xaml
    /// </summary>
    public partial class GateUI : UserControl
    {
        /* DependencyProperty */
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(String), typeof(GateUI), null);
        public static readonly DependencyProperty GateProperty = DependencyProperty.Register("Gate", typeof(Gate), typeof(GateUI), null);
        public static readonly DependencyProperty StageProperty = DependencyProperty.Register("Stage", typeof(Stage), typeof(GateUI), null);
        public String Id
        {
            get { return (String)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public Gate Gate
        {
            get { return (Gate)GetValue(GateProperty); }
            set { SetValue(GateProperty, value); }
        }
        public Stage Stage
        {
            get { return (Stage)GetValue(StageProperty); }
            set { SetValue(StageProperty, value); }
        }
        public GateItem[] GateItems
        {
            get
            {
                if (Stage == null)
                    return null;
                GateItem[] GateItems = new GateItem[Stage.Types.Length];
                for (int i = 0; i < GateItems.Length; i++)
                    GateItems[i] = new GateItem(Stage.Types[1], GateTypes.EMPTY);
                return GateItems;
            }
        }
        public int SelectedIndex { get; set; }

        public ImageSource SelectedImage
        {
            get { return GateItems[SelectedIndex].Image; }
        }

        public GateUI()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
