using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebUI.Controllers
{
    public class CartItemController : Controller
    {
        public IActionResult Remove(int id){
            Cart cart = HttpContext.Session.GetObject<Cart>("cart");
        
            cart.RemoveCartItemByProductId(id);

            HttpContext.Session.SetObject("cart", cart);

            return Redirect("/Cart");
        }
    }
}