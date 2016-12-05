using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WallET.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page product template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WallET.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WalletPage : Page
    {


        //Set up SQLite DB + path
        //global vars for balance, current balance and total cost.
        private string path;
        private SQLite.Net.SQLiteConnection conn;
        public double balance;
        public double currBalance;
        double totalCost = 0;

        public WalletPage()
        {
            //path for Database + Creating Table
            this.InitializeComponent();
            this.SetBalance();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.budgetControl");
            dbConnection();
            conn.CreateTable<Product>();
            closeDBconnection();

            this.checkFileContent();
        }





        private void SetBalance()
        {
            //curent userSetBalance. 
            currBalance = balance - totalCost;
            curentBalanceBox.Text = currBalance.ToString();
        }
        private async void balanceStorage()
        {
            //get a link to current folder
            StorageFolder balanceFolder = ApplicationData.Current.LocalFolder;
            //file userSetBalance
            StorageFile userSetBalance;

            try
            {
                //Gets file, content and puts it into string.
                userSetBalance = await balanceFolder.GetFileAsync("balance.txt");
            }
            catch (Exception E)
            {
                //If the file does not exist, create it.
                string message = E.Message;
                userSetBalance = await balanceFolder.CreateFileAsync("balance.txt");
            }
            //Add file content
            string output;
            output = balance.ToString();
            //write to file the original contents, also output of balance
            await Windows.Storage.FileIO.WriteTextAsync(userSetBalance, output);
            //SHow message
            await new MessageDialog("saved to file").ShowAsync();
        }

        private async void checkFileContent()
        {
            //get a link to current folder and read from locations.txt file
            StorageFolder balanceFolder = ApplicationData.Current.LocalFolder;

            StorageFile userSetBalance;
            try
            {
                //get the file and its contents to a string
                userSetBalance = await balanceFolder.GetFileAsync("balance.txt");
            }
            catch (Exception E)
            {
                string message = E.Message;
                return;
            }
            //read from the file
            string fileText = await Windows.Storage.FileIO.ReadTextAsync(userSetBalance);
            //output file contents to textblock
            balanceBox.Text = fileText;

            balance = Convert.ToDouble(fileText);


        }

        private async void btnSetBalance_Click(object sender, RoutedEventArgs e)
        {
            if (txtBalanceInput.Text.ToString() != "")
            {
                try
                {
                    balance = Convert.ToDouble(txtBalanceInput.Text);
                    balanceBox.Text = balance.ToString();

                    SetBalance();
                    balanceStorage();
                }
                catch
                {
                    MessageDialog Dialog = new MessageDialog("Input must be numeric");
                    await Dialog.ShowAsync();
                }

            }

        }





        //ADD
        //create SQLite DB connection
        //create variable "addProduct" that holds SQLite insert statement
        //set each instance variable in the Item class with values from each TextBox
        //Call Show_tapped func to asynchronously update page
        //close db conn
        //clear textboxes after adding
        private async void Add_Tapped(object sender, TappedRoutedEventArgs e)
        {
            dbConnection();
            try
            {

                if (!IsDecimal(quantityTextBox) || !IsDecimal(priceTextBox))
                {
                    MessageDialog decimalMsg = new MessageDialog("Quantity + Price must be a number.");
                    await decimalMsg.ShowAsync();
                }
                else
                {
                    var addProduct = conn.Insert(new Product()
                    {
                        productName = productTextBox.Text,
                        productQTY = Convert.ToInt32(quantityTextBox.Text),
                        productPrice = Convert.ToDouble(priceTextBox.Text)
                    });
                }
                

              
                
            }
            catch (Exception)
            {
                MessageDialog message = new MessageDialog("Nothing to Add.");
                await message.ShowAsync();
            }
            
            //ShowAllError_Tapped(sender, e);   for showing all data when using Add_Tapped
            closeDBconnection();
            //clearTextBoxes();
            Show_Tapped(sender, e);
            SetBalance();

        } // End Add_Tapped




        //SHOW/RETRIEVE
        //connect to DB
        //create a List of products and populate it with values from Item Table
        //display all in ListBox
        //close db connection
        private void Show_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                dbConnection();
                List<string> products = new List<string>();
                var listProducts = conn.Table<Product>();
                string result = string.Empty;
                //double totalCost = 0;

                totalCost = 0;
                foreach (var product in listProducts)
                {
                    result = string.Format("{0} " + Environment.NewLine + " Qty: {1}     Price: €{2}", product.productName, product.productQTY, product.productPrice);
                    products.Add(result);
                    totalCost += Convert.ToDouble(product.productPrice);
                }

                if (products.Count == 0)
                {

                        listBoxTotalCost.Text = "Your List is Empty!";
                    
                }
                else if (products.Count > 0)
                {
                    listBoxTotalCost.Text = "";
                }
                fetchData.DataContext = products;


                listBoxTotalItemCount.Text = ("Product Count: " + Convert.ToString(products.Count));
                listBoxTotalCost.Text = ("Total Cost: € " + totalCost);


                SetBalance();
                closeDBconnection();
            }
            catch (Exception)
            {
                throw;
            }

        } // End SHOW ALL_Tapped


        private void Delete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                dbConnection();
                var listProducts = conn.Table<Product>();
                List<string> products = new List<string>();
                foreach (var product in listProducts)
                {
                    string compareResult = string.Format("{0} " + Environment.NewLine + " Qty: {1}     Price: €{2}", product.productName, product.productQTY, product.productPrice);

                    if (fetchData.SelectedValue.Equals(compareResult))
                    {
                        conn.Delete<Product>(product.productID);
                    }
                }
                Show_Tapped(sender, e);
                closeDBconnection();
            }
            catch (Exception)
            {
                throw;
            }
        } //  End Function


        private async void DeleteAll_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                dbConnection();
                var listProducts = conn.Table<Product>();
                List<string> products = new List<string>();

                foreach (var product in listProducts)
                {
                    conn.Delete<Product>(product.productID);
                }
                Show_Tapped(sender, e);
                closeDBconnection();

                MessageDialog message = new MessageDialog("All Items Removed");
                await message.ShowAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Checks to ensure that only Numeric data is allowed in Qty and Price Fields, returns false if not
        private bool IsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void fetchData_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Delete.IsEnabled = true;
            DeleteAll.IsEnabled = true;
        }

        private void fetcheData_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Delete.IsEnabled = false;
            DeleteAll.IsEnabled = false;
        }

        //Database conn, conn variable + path of where db i.
        private void dbConnection()
        {
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
        }


        // Close database connection
        private void closeDBconnection()
        {
            conn.Commit();
            conn.Dispose();
            conn.Close();
        }

        private async void SaveReceipt_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFolder receiptFolder = ApplicationData.Current.LocalFolder;

            StorageFile receiptFile;
            string fileText = "";

            try
            {
                receiptFile = await receiptFolder.GetFileAsync("receipt.txt");
                fileText = await Windows.Storage.FileIO.ReadTextAsync(receiptFile);
            }
            catch (Exception E)
            {
                string message = E.Message;
                receiptFile = await receiptFolder.CreateFileAsync("receipt.txt");
            }

            try
            {
                dbConnection();
                List<string> products = new List<string>();
                var listProducts = conn.Table<Product>();
                string result = string.Empty;
                //double totalCost = 0;

                DateTime thisDay = DateTime.Today;
                string TimeStamp = thisDay.ToString();

                totalCost = 0;
                int count = 0;

                foreach (var product in listProducts)
                {
                    result = string.Format("{0} " + Environment.NewLine + " Qty: {1}     Price: €{2}", product.productName, product.productQTY, product.productPrice);
                    products.Add(result);
                    totalCost += Convert.ToDouble(product.productPrice);
                    if (count == 0)
                    {
                        await Windows.Storage.FileIO.WriteTextAsync(receiptFile, fileText += System.Environment.NewLine + System.Environment.NewLine + TimeStamp + System.Environment.NewLine + result);
                        count++;
                    }else
                    {
                        await Windows.Storage.FileIO.WriteTextAsync(receiptFile, fileText += System.Environment.NewLine + result);
                    }
                }
                fetchData.DataContext = products;
                listBoxTotalItemCount.Text = ("Product Count: " + Convert.ToString(products.Count));
                listBoxTotalCost.Text = ("Total Cost: € " + totalCost);
                closeDBconnection();
            }
            catch (Exception)
            {
                throw;
            }

            MessageDialog clearDialog = new MessageDialog("Receipt Saved");
            await clearDialog.ShowAsync();
        }

        private void gotoRec_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReceiptPage));
        }
    }
}
