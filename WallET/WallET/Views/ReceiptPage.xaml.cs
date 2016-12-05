using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class ReceiptPage : Page
    {
        public ReceiptPage()
        {
            this.InitializeComponent();
            this.DisplayReceipt();
        }

        private async void DisplayReceipt()
        {
            //if no text make it invisible
            if (receiptDisplay.Text.ToString() == "")
            {
                btnClear.Visibility = Visibility.Visible;
            }
            //manage folder + content + show info about them
            //creating StorageFile
            StorageFolder receiptFolder = ApplicationData.Current.LocalFolder;
            StorageFile receiptFile;


            try
            {
                receiptFile = await receiptFolder.GetFileAsync("receipt.txt");

            }
            catch (Exception E)
            {
                string message = E.Message;
                receiptDisplay.Text = "No receipts found";
                return;
            }
            //reading from the file weatherFIle
            string fileText = await Windows.Storage.FileIO.ReadTextAsync(receiptFile);


            //print content to txtBox weatherDisplay
            receiptDisplay.Text = receiptDisplay.Text + fileText;


        }

        private async void btnClear_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFolder receiptFolder = ApplicationData.Current.LocalFolder;

            StorageFile receiptFile;


            try
            {
                receiptFile = await receiptFolder.GetFileAsync("receipt.txt");

            }
            catch (Exception E)
            {
                string message = E.Message;
                receiptDisplay.Text = "File with saved receipt does not exist";
                return;
            }

            string emptyText = "";
            await Windows.Storage.FileIO.WriteTextAsync(receiptFile, emptyText);

            receiptDisplay.Text = "";

            MessageDialog clearDialog = new MessageDialog("Data Cleared");
            await clearDialog.ShowAsync();
        }
    }

}

