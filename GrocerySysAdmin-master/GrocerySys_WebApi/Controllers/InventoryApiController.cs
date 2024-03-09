using GrocerySys_DAL.Models;
using GrocerySys_DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GrocerySys_WebApi.Controllers
{
    public class InventoryApiController : ApiController
    {
        InventoryRepository InvREPO = new InventoryRepository();


        [HttpPost]
        public void insertProduct(InventoryModel PDT)
        {
            InvREPO.insertProduct(PDT);
        }

        [HttpGet]
        public List<InventoryModel> getProducts() 
        { 
            return InvREPO.getProducts();
        }

        [HttpDelete]
        public void deleteProduct(string ProductID)
        {
            InvREPO.deleteProduct(ProductID);
        }
    }
}
