using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using WallET.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WallET
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Storage for registration
        IsolatedStorageFile ISOFile = IsolatedStorageFile.GetUserStoreForApplication();
        List<UserData> UserListDataObject = new List<UserData>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        //Registration Submit click event
        public async void Submit_Click(object sender, RoutedEventArgs e)
        {
            //UserName Validation   
            //throw dialog if username is invalid (if any of chars specified below are used)
            if (!Regex.IsMatch(TxtUserName.Text.Trim(), @"^[A-Za-z_][a-zA-Z0-9_\s]*$"))
            {
                //dialog "regUserDialog" that will be thrown if input is different than specified i.e using @ or # symbols.
                MessageDialog regUserDialog = new MessageDialog("Invalid Username.");
                await regUserDialog.ShowAsync();
            }
            //Password length Validation (min of 6 chars)
            else if (TxtPwd.Password.Length < 6)
            {
                MessageDialog regPassDialog = new MessageDialog("Password length should be minimum of 6 characters!");
                await regPassDialog.ShowAsync();
            }
            //After validation success ,store user detials in isolated storage   
            else if (TxtUserName.Text != "" && TxtPwd.Password != "")
            {
                UserData UserDataObject = new UserData();
                UserDataObject.UserName = TxtUserName.Text;
                UserDataObject.Password = TxtPwd.Password;
                int Choice = 0;
                foreach (var UserLogin in UserListDataObject)
                {
                    if (UserDataObject.UserName == UserLogin.UserName)
                    {
                        Choice = 1;
                    }
                }//end foreach

                //Checking existing user names in Isolated Storage DB   
                if (Choice == 0)
                {
                    UserListDataObject.Add(UserDataObject);
                    //If file RegDB exists then delete it.
                    if (ISOFile.FileExists("RegDB"))
                    {
                        ISOFile.DeleteFile("RegDB");
                    }//Creating RegDB file (incl. XML serialization/deserialization using UserData Model)
                    using (IsolatedStorageFileStream fileStream = ISOFile.OpenFile("RegDB", FileMode.Create))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(List<UserData>));
                        serializer.WriteObject(fileStream, UserListDataObject);
                    }
                    //Success Notification for registration
                    MessageDialog regSuccDialog = new MessageDialog("Successful Registration.");
                    await regSuccDialog.ShowAsync();
                }
                else
                {
                    //Notification about taken username
                    MessageDialog regExistDialog = new MessageDialog("Username is already taken.");
                    await regExistDialog.ShowAsync();
                }
            }

        }//submit registration click

    }
}
