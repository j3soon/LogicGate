using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LogicGate;

namespace LogicGateUI
{
    public class Images
    {
        private static Images _instance;
        public static Images Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                return _instance = new Images();
            }
        }
        public Dictionary<GateTypes, ImageSource> GateImages { get; set; } = new Dictionary<GateTypes, ImageSource>();

        public Images()
        {
            Uri baseUri = BaseUriHelper.GetBaseUri(MainWindow.Instance);
            String basePath = "/LogicGateUI;component/Resources/";
            foreach (GateTypes t in Enum.GetValues(typeof(GateTypes)))
            {
                if (t != GateTypes.NOT && t != GateTypes.TRUE && t != GateTypes.FALSE)
                    GateImages.Add(t, new BitmapImage(new Uri(baseUri, basePath + t + ".png")));
            }
        }
    }
}
