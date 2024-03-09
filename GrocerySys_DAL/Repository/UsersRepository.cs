using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class UsersRepository
    {
        public List<LoginModel> getAllUsers()
        {

            List<LoginModel> UserList = new List<LoginModel>();
            using (SqlConnection con = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getAdminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        LoginModel loginData = new LoginModel();
                        loginData.AdminID = SDR["AdminID"].ToString();
                        loginData.AdminName = SDR["AdminName"].ToString();
                        loginData.AdminRole = SDR["AdminRole"].ToString();
                        loginData.AdminPassword = SDR["AdminPassword"].ToString();
                        loginData.AdminPhoto = (byte[])SDR["AdminPhoto"];
                        UserList.Add(loginData);
                    }
                }
            }
            return UserList;
        }


        public void deleteUser(string AdminID)
        {
            using (SqlConnection con = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_deleteAdminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", AdminID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}