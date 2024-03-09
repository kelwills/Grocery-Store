using GrocerySys_DAL;
using GrocerySys_DAL.Models;
using GrocerySys_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace GrocerySys_WebApi.Controllers
{
    public class LoginApiController : ApiController
    {
        LoginRepository LogREP = new LoginRepository();
        UsersRepository UserREP = new UsersRepository();

        [HttpPost]
        public bool getUserInfo(string ID, string Password)
        {
            return LogREP.getUserInfo(ID, Password);
        }


        [HttpGet]
        public LoginModel getUserData(string ID)
        {
            return LogREP.getUserdata(ID);
        }

        [HttpGet]
        public List<LoginModel> getAllUsers()
        {
            return UserREP.getAllUsers();
        }

        [HttpDelete]
        public void deleteUser(string AdminID)
        {
            UserREP.deleteUser(AdminID);
        }
    }
}
