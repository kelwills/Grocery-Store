using GrocerySys_DAL.Models;
using GrocerySys_DAL.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace GrocerySys_WebApi.Controllers
{
    public class CartApiController : ApiController
    {
        CartRepository CartREPO = new CartRepository();

        [HttpPost]
        public void addItems(string CustomerID, string ProductID)
        {
            CartREPO.addItems(CustomerID, ProductID);
        }

        public List<getCartModel> getCart()
        {
            return CartREPO.getCart();
        }
    }

    
}
