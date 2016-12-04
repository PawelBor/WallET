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

            //Dunnes Stores Galway
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

            shp.Add(new Shops()
            {
                Name = "Dunnes Stores",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.283563,
                    Longitude = -9.044815
                })
            });

            shp.Add(new Shops()
            {
                Name = "Dunnes Stores",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.274875,
                    Longitude = -9.050687
                })
            });

            shp.Add(new Shops()
            {
                Name = "Dunnes Stores",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.275407,
                    Longitude = -9.050386
                })
            });

            shp.Add(new Shops()
            {
                Name = "Dunnes Stores",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.290542,
                    Longitude = -8.987148
                })
            });
            //END Dunnes Stores

            //TESCO
            shp.Add(new Shops()
            {
                Name = "TESCO Express",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.293793,
                    Longitude = -9.036940
                })
            });

            shp.Add(new Shops()
            {
                Name = "TESCO",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.280103,
                    Longitude = -9.047879
                })
            });

            shp.Add(new Shops()
            {
                Name = "TESCO",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.269859,
                    Longitude = -8.930677
                })
            });

            //END TESCO

            //Aldi
            //aldi city
            shp.Add(new Shops()
            {
                Name = "ALDI",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.281106,
                    Longitude = -9.050084
                })
            });
            //aldi westside
            shp.Add(new Shops()
            {
                Name = "ALDI",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.276771,
                    Longitude = -9.072242
                })
            });

            //aldi knocknacarra
            shp.Add(new Shops()
            {
                Name = "ALDI",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.271014,
                    Longitude = -9.097997
                })
            });

            //LIDL 
            //lidl headford
            shp.Add(new Shops()
            {
                Name = "LIDL",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.278520,
                    Longitude = -9.049557
                })
            });
            //lidl daughiska
            shp.Add(new Shops()
            {
                Name = "LIDL",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.284546,
                    Longitude = -8.980239
                })
            });
            //lidl oranmore
            shp.Add(new Shops()
            {
                Name = "LIDL",
                ImageSourceUri = new Uri("ms-appx:///Images/mappin.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 53.264263,
                    Longitude = -8.926252
                })
            });

            return shp;
        }
    }
}
