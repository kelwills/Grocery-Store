using GrocerySys_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace GrocerySys_MVC.Controllers
{
    public class SignUpController : Controller
    {
        string apiURL = "https://localhost:44327/api";
        // GET: SignUp
        public ActionResult signUp()
        {
            LoginModel UserData;
            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {

                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/LoginApi?ID={Session["ID"] as string}");
                    HttpResponseMessage response = client.SendAsync(request).Result;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    UserData = serializer.Deserialize<LoginModel>(response.Content.ReadAsStringAsync().Result);
                }
                ViewBag.CurrentID = UserData.AdminID;
                ViewBag.CurrentName = UserData.AdminName;
                ViewBag.CurrentRole = UserData.AdminRole;
                ViewBag.CurrentPhoto = UserData.AdminPhoto;
            }
                return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddUserAsync(SignUpModel User , HttpPostedFileBase AdminPic)
        {
            byte[] imageBytes;

            using (BinaryReader reader = new BinaryReader(AdminPic.InputStream))
            {
                imageBytes = reader.ReadBytes(AdminPic.ContentLength);
            }
            User.AdminPhoto = imageBytes;
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(User);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{apiURL}/SignUpApi", data);
                return RedirectToAction("MainDash", "Dashboard");
            }
        }
    }
}