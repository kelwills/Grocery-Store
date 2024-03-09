using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class SignUpRepository
    {
        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
        public void addAdmin(SignUpModel User)
        {
            using(SqlConnection Conn = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_insertAdminLogin", Conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", User.AdminID);
                cmd.Parameters.AddWithValue("@name", User.AdminName);
                cmd.Parameters.AddWithValue("@role", User.AdminRole);
                cmd.Parameters.AddWithValue("@password", EnryptString(User.AdminPassword));
                cmd.Parameters.AddWithValue("@photo", User.AdminPhoto);
                cmd.ExecuteNonQuery();
            }
        }
    }
}