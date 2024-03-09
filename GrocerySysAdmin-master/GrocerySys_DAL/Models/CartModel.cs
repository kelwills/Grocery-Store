using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Models
{
    public class CartModel
    {
        public int CartID { get; set; }
        public string CustomerID { get; set;}
        public string ProductID { get; set;}
        public string Quantity { get; set;}
        public string Price { get; set;}
        public string Total { get; set;}
    }
}