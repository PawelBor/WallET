using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace WallET.Model
{
    class Shops
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int NumSpaces { get; set; }
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }

        public Shops()
        {
            this.NormalizedAnchorPoint = new Point(0, 0);
        }
    }
}
