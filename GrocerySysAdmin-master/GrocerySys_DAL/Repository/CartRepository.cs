using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class CartRepository
    {
        public void addItems(string CustomerID , string ProductID)
        {
            using(SqlConnection con = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_insertCartItems", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerID", CustomerID);
                cmd.Parameters.AddWithValue("@productID", ProductID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<getCartModel> getCart()
        {
            List<getCartModel> cartList = new List<getCartModel>();
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getCart" , conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using(SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while(SDR.Read())
                    {
                        getCartModel cart = new getCartModel();
                        cart.GrandTotal = Convert.ToInt32(SDR["GrandTotal"]);
                        cart.UserName = SDR["UserName"].ToString();
                        cart.UserAddress = SDR["UserAddress"].ToString();
                        cart.ProductName = SDR["ProductNames"].ToString();
                        cart.Quantities = SDR["Quantities"].ToString();
                        cartList.Add(cart); 
                    }
                }
            }
            return cartList;
        }
    }
}