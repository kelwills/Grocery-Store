using GrocerySys_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GrocerySys_MVC.Controllers
{
    public class DashboardController : Controller
    {

        string apiURL = "https://localhost:44327/api";

        public void side()
        {
            LoginModel UserData;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/LoginApi?ID={Session["ID"] as string}");
                HttpResponseMessage response = client.SendAsync(request).Result;
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                UserData = JsonConvert.DeserializeObject<LoginModel>(jsonResponse);
            }
            ViewBag.CurrentID = UserData.AdminID;
            ViewBag.CurrentName = UserData.AdminName;
            ViewBag.CurrentRole = UserData.AdminRole;
            ViewBag.CurrentPhoto = UserData.AdminPhoto;
        }


        public ActionResult MainDash()
        {
           
            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {

                side();

/////////////////////////////////////////////////////////////////////////////////////////

                List<LoginModel> UserList = new List<LoginModel>();
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/LoginApi");
                    HttpResponseMessage response = client.SendAsync(request).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        // Ensure successful response before attempting deserialization
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // Use Newtonsoft.Json for deserialization (Json.NET)
                        UserList = JsonConvert.DeserializeObject<List<LoginModel>>(jsonResponse);
                    }

                }
                ViewBag.UserList = UserList;

                ////////////////////////////////////////////////////////////////////////////////////////

                List<InventoryModel> ProductList = new List<InventoryModel>();
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/InventoryApi");
                    HttpResponseMessage response = client.SendAsync(request).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        // Ensure successful response before attempting deserialization
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // Use Newtonsoft.Json for deserialization (Json.NET)
                        ProductList = JsonConvert.DeserializeObject<List<InventoryModel>>(jsonResponse);
                    }

                }
                ViewBag.ProductList = ProductList;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                List<ImageModel> ImageList = new List<ImageModel>();
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/ImageApi");
                    HttpResponseMessage response = client.SendAsync(request).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        // Ensure successful response before attempting deserialization
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // Use Newtonsoft.Json for deserialization (Json.NET)
                        ImageList = JsonConvert.DeserializeObject<List<ImageModel>>(jsonResponse);
                    }

                }
                ViewBag.ImageList = ImageList;




                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                List<getCartModel> CartList = new List<getCartModel>();
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{apiURL}/CartApi");
                    HttpResponseMessage response = client.SendAsync(request).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        // Ensure successful response before attempting deserialization
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        // Use Newtonsoft.Json for deserialization (Json.NET)
                        CartList = JsonConvert.DeserializeObject<List<getCartModel>>(jsonResponse);
                    }

                }
                ViewBag.CartList = CartList;



                return View();
            }
            return RedirectToAction("Login" , "login");
        }


        [HttpPost]
        public ActionResult logOut()
        {
            Session["ID"] = null;
            Session["IsLoggedIn"] = false;
            return RedirectToAction("Login", "login");
        }

        [HttpPost]
        public async Task<ActionResult> deleteUserAsync(string AdminID)
        {

            if (!string.IsNullOrEmpty(Session["ID"] as string))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{apiURL}/LoginApi?AdminID={AdminID}");
                    HttpResponseMessage response = client.SendAsync(request).Result;
                    return RedirectToAction("MainDash");
                }
            }

            return RedirectToAction("Login", "login");

        }


        [HttpPost]
        public async Task<ActionResult> deleteProductAsync(string ProductID)
        {

            if (!string.IsNullOrEmpty(Session["ID"] as string))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{apiURL}//InventoryApi?ProductID={ProductID}");
                    HttpResponseMessage response = client.SendAsync(request).Result;
                    return RedirectToAction("MainDash");
                }
            }

            return RedirectToAction("Login", "login");

        }

       


        [HttpPost]
        public ActionResult redToDash()
        {
            return RedirectToAction("MainDash");
        }

        [HttpPost]
        public ActionResult redToAddUser()
        {
            return RedirectToAction("signUp", "SignUp");
        }




        public ActionResult AddProduct()
        {
            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {
                side();
            }
            return View() ;
        }

        [HttpPost]
        public async Task<ActionResult> AddProductAsync(InventoryModel Product , HttpPostedFileBase ProductPhoto)
        {
            byte[] imageBytes;

            using (BinaryReader reader = new BinaryReader(ProductPhoto.InputStream))
            {
                imageBytes = reader.ReadBytes(ProductPhoto.ContentLength);
            }
            Product.ProductImage = imageBytes;
            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(Product);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{apiURL}/InventoryApi", data);
                    return RedirectToAction("MainDash", "Dashboard");
                }
            }

            return RedirectToAction("Login", "login");
        }



        public ActionResult AddImage()
        {
            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {
                side();
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddImageAsync(ImageModel Img , HttpPostedFileBase Image)
        {
            byte[] imageBytes;

            using (BinaryReader reader = new BinaryReader(Image.InputStream))
            {
                imageBytes = reader.ReadBytes(Image.ContentLength);
            }
            Img.ImgData = imageBytes;

            if ((!string.IsNullOrEmpty(Session["ID"] as string)) && ((Session["IsLoggedIn"] as bool?) ?? true))
            {
                using (HttpClient client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(Img);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"{apiURL}/ImageApi", data);
                    return RedirectToAction("MainDash", "Dashboard");
                }
            }

            return RedirectToAction("Login", "login");
        }


        [HttpPost]
        public async Task<ActionResult> deleteImageAsync(string ImgID)
        {

            if (!string.IsNullOrEmpty(Session["ID"] as string))
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{apiURL}//ImageApi?ImgID={ImgID}");
                    HttpResponseMessage response = client.SendAsync(request).Result;
                    return RedirectToAction("MainDash");
                }
            }

            return RedirectToAction("Login", "login");

        }
    }
}