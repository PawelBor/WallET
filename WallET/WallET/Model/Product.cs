using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallET.Model
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int productID { get; set; }
        public string productName { get; set; }
        public int productQTY { get; set; }
        public double productPrice { get; set; }
    }
}
