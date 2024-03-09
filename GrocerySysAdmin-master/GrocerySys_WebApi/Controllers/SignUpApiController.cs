using GrocerySys_DAL.Models;
using GrocerySys_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GrocerySys_DAL.Repository;

namespace GrocerySys_WebApi.Controllers
{
    public class SignUpApiController : ApiController
    {
        SignUpRepository SREP = new SignUpRepository();
        public void addAdmin(SignUpModel User)
        {
            SREP.addAdmin(User);
        }
    }
}
