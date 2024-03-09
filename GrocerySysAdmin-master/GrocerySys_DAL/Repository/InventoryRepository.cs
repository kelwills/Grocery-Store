using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class InventoryRepository
    {
        public void insertProduct(InventoryModel PDT)
        {
            using(SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_insertProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", PDT.ProductID);
                cmd.Parameters.AddWithValue("@name", PDT.ProductName);
                cmd.Parameters.AddWithValue("@type", PDT.ProductType);
                cmd.Parameters.AddWithValue("@qty", PDT.ProductQuantity);
                cmd.Parameters.AddWithValue("@brand", PDT.ProductBrand);
                cmd.Parameters.AddWithValue("@price", PDT.ProductPrice);
                cmd.Parameters.AddWithValue("@image", PDT.ProductImage);
                cmd.ExecuteNonQuery();
            }
        }


        public List<InventoryModel> getProducts()
        {
            List<InventoryModel> ProductList = new List<InventoryModel>();

            using(SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using(SqlDataReader SDR =  cmd.ExecuteReader())
                {
                    while(SDR.Read())
                    {
                        InventoryModel Product = new InventoryModel();
                        Product.ProductID = SDR["ProductID"].ToString();
                        Product.ProductName = SDR["ProductName"].ToString();
                        Product.ProductType = SDR["ProductType"].ToString();
                        Product.ProductQuantity = Convert.ToInt32(SDR["ProductQuantity"]);
                        Product.ProductBrand = SDR["ProductBrand"].ToString();
                        Product.ProductPrice = Convert.ToInt32(SDR["ProductPrice"]); 
                        Product.ProductImage = (byte[])SDR["ProductImage"]; 
                        ProductList.Add(Product);
                    }
                }
            }
            return ProductList;
        }


        public void deleteProduct(string ProductID)
        {
            using (SqlConnection conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_deleteProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ProductID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}