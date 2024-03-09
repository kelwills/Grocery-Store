using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Models
{
    public class getCartModel
    {
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string ProductName { get; set; }
        public string Quantities { get; set; }
        public int GrandTotal { get; set; }
    }
}