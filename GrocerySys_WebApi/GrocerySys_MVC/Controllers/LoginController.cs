using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GrocerySys_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        string apiURL = "https://localhost:44327/api/LoginApi";
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult verifyLogin(string ID , string Password)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{apiURL}?ID={ID}&Password={Password}");
                HttpResponseMessage response = client.SendAsync(request).Result;

                bool isSuccess = (new JavaScriptSerializer()).Deserialize<bool>(response.Content.ReadAsStringAsync().Result);

                if (isSuccess)
                { 
                    Session["ID"] = ID;
                    Session["IsLoggedIn"] = true;
                    return RedirectToAction("MainDash", "Dashboard");
                }

                ViewBag.ErrorMessage = "Invalid credentials";
                return RedirectToAction("login");
            }
        }
    }
}