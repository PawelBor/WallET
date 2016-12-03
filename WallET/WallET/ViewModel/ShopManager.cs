using System;
using System.Collections.Generic;
using WallET.Model;
using Windows.Devices.Geolocation;

namespace WallET.ViewModel
{
    class ShopManager
    {
        public List<Shops> GetShop()
        {
            List<Shops> shp = new List<Shops>();

            shp.Add(new Shops()
            {
                Name = "Dunnes Stores",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.2731238,
                    Longitude = -9.0996908
                })
            });

            shp.Add(new Shops()
            {
                Name = "B&Q",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.2716725,
                    Longitude = -9.098981
                })
            });

            return shp;
        }
    }
}
