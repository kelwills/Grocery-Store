using GrocerySys_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace GrocerySys_DAL.Repository
{
    public class LoginRepository
    {

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public bool getUserInfo(string ID, string Password)
        {
            LoginModel loginData = getUserdata(ID);

            string Passw = EnryptString(Password);

            if (ID == loginData.AdminID && loginData.AdminPassword == Passw)
            {
                HttpCookie userCookie = new HttpCookie("UserAuthentication");
                userCookie["ID"] = loginData.AdminID;
                userCookie["Password"] = loginData.AdminPassword;
                userCookie.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(userCookie);
                return true;
            }
            return false;
        }

        public LoginModel getUserdata(string ID)
        {
            LoginModel loginData = new LoginModel();
            using (SqlConnection con = clsConnectionDB.openConnection())
            {
                SqlCommand cmd = new SqlCommand("Proc_getByIDAdminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {
                        loginData.AdminID = SDR["AdminID"].ToString();
                        loginData.AdminName = SDR["AdminName"].ToString();
                        loginData.AdminRole = SDR["AdminRole"].ToString();
                        loginData.AdminPassword = SDR["AdminPassword"].ToString();
                        loginData.AdminPhoto = (byte[])SDR["AdminPhoto"];
                    }
                }
            }

            return loginData;
        }


    }
}