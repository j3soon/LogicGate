using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            String basePath = "../../Resources/";
            foreach (GateTypes t in Enum.GetValues(typeof(GateTypes)))
            {
                String path = basePath + t + ".png";
                if (File.Exists(path))
                    GateImages.Add(t, new BitmapImage(new Uri(Path.GetFullPath(path))));
            }
        }
    }
}
