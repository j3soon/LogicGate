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
    public static class Images
    {
        public static Dictionary<GateTypes, ImageSource> GateImages { get; set; } = new Dictionary<GateTypes, ImageSource>();

        static Images()
        {
            String basePath = "/LogicGateUI;component/Resources/";
            foreach (GateTypes t in Enum.GetValues(typeof(GateTypes)))
            {
                String path = basePath + t + ".png";
                if (File.Exists(path))
                    GateImages.Add(t, new BitmapImage(new Uri(path)));
            }
        }
    }
}
