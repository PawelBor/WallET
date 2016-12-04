using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WallET.Model;
using WallET.ViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WallET.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
   
    public sealed partial class MapPage : Page
    {
        ShopManager storeManager;

        public MapPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            StoreMap.MapServiceToken = "LjAtiC5aNFPpcxSyy29M~HCQIpzUD7UFVDw77JI1Xmg~Ai8yD06vGSyqxPxKwhV2yLuG4eanMbZEphNpVsJwSt-r7cWmt3f46xM-ka1PVNsC";
            var locator = new Geolocator();
            //sets accuracy to 100 (Best accuracy)
            locator.DesiredAccuracyInMeters = 100;
            //current location set in "position" variable
            var position = await locator.GetGeopositionAsync();
            await StoreMap.TrySetViewAsync(position.Coordinate.Point, 18D);
            //if position is not empty get lat, long and assign it to icon.Location and add it to the map
            if (position != null)
            {
                Geopoint myPos = new Geopoint(new BasicGeoposition()
                {
                    Latitude = position.Coordinate.Latitude,
                    Longitude = position.Coordinate.Longitude
                });

                MapIcon icon = new MapIcon();
                icon.Location = myPos;
                icon.Title = "Home";
                StoreMap.MapElements.Add(icon);
            }
            //get all the shops added in ShopManager VM
            storeManager = new ShopManager();


        }

        //gets all shops from GetShop List from SHopManager and displays on map
        private void showStore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MapSrc.ItemsSource = storeManager.GetShop();
        }

        private void mapSrcBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var shopIcon = sender as Button;
            Shops shop = shopIcon.DataContext as Shops;
        }
    }
}
