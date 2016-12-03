using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using WallET.Model;
using WallET.Views;
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
        List<PersonalData> PersonalListDataObject = new List<PersonalData>();

        public MainPage()
        {
            this.InitializeComponent();
            //Run MainPage_Loaded when MainPage is accessed by the user.
            this.MainPage_Loaded();
        }

        //Doing this because in order to compare data entered with existing data (username/password)
        //Load the file RegDB when MainPage is loaded. (For existing account purpose)
        public void MainPage_Loaded()
        {
            //determines whether specified path refers to existing file. It's an Isolated Sotrage File -> AppData
            if (ISOFile.FileExists("RegDB"))
            {
                //ISFS Allows for IsolatedFile Exposure. Opens RegDB.
                using (IsolatedStorageFileStream fileStream = ISOFile.OpenFile("RegDB", FileMode.Open))
                {
                    //Serializes and deserializes an instance of a type into an XML stream or document using a supplied data contract
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<PersonalData>));
                    PersonalListDataObject = (List<PersonalData>)serializer.ReadObject(fileStream);
                }
            }
        }//MainPage Load END


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
                PersonalData PersonalDataObject = new PersonalData();
                PersonalDataObject.UserName = TxtUserName.Text;
                PersonalDataObject.Password = TxtPwd.Password;
                int Choice = 0;
                foreach (var UserLogin in PersonalListDataObject)
                {
                    if (PersonalDataObject.UserName == UserLogin.UserName)
                    {
                        Choice = 1;
                    }
                }//end foreach

                //Checking existing user names in Isolated Storage DB   
                if (Choice == 0)
                {
                    PersonalListDataObject.Add(PersonalDataObject);
                    //If file RegDB exists then delete it.
                    if (ISOFile.FileExists("RegDB"))
                    {
                        ISOFile.DeleteFile("RegDB");
                    }//Creating RegDB file (incl. XML serialization/deserialization using PersonalData Model)
                    using (IsolatedStorageFileStream fileStream = ISOFile.OpenFile("RegDB", FileMode.Create))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(List<PersonalData>));
                        serializer.WriteObject(fileStream, PersonalListDataObject);
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

        }//END submit registration click



        public async void Login_Click(object sender, RoutedEventArgs e)
        {
            //If Username / password txtBox is empty throw "logEnterDetailsDialog" Message Dialog.
            if (UserName.Text != "" && PassWord.Password != "")
            {
                int Choice = 0;
                //for each UserLogin in ObjUserDataList
                foreach (var UserLogin in PersonalListDataObject)
                {
                    //if User Input is same as UserLogin data in PersonalListDataObject set Choice to 1...
                    if (UserName.Text == UserLogin.UserName && PassWord.Password == UserLogin.Password)
                    {
                        Choice = 1;

                        //If Isolated FIle "TempLoginPersonalData" exists Delete it.(way of overwriting a file)
                        if (ISOFile.FileExists("TempLoginPersonalData"))
                        {
                            ISOFile.DeleteFile("TempLoginPersonalData");
                        }
                        // Create "TempLoginPersonalData"
                        using (IsolatedStorageFileStream fileStream = ISOFile.OpenFile("TempLoginPersonalData", FileMode.Create))
                        {
                            ////Serializes and deserializes an instance of a type into an XML stream or document using a supplied data contract
                            DataContractSerializer serializer = new DataContractSerializer(typeof(PersonalData));

                            serializer.WriteObject(fileStream, UserLogin);

                        }
                        //If Username + Password is correct navigate to MenuPage
                        Frame.Navigate(typeof(ChoicePage));
                    }
                }
                //If Choice is set to 0 throw Message Dialog "logInvalidDialog"
                if (Choice == 0)
                {
                    MessageDialog logInvalidDialog = new MessageDialog("Invalid UserID/Password");
                    await logInvalidDialog.ShowAsync();
                }
            }
            else
            {
                //same as
                MessageDialog logEnterDetailsDialog = new MessageDialog("Enter UserID/Password");
                await logEnterDetailsDialog.ShowAsync();
            }

        }

    }
}
