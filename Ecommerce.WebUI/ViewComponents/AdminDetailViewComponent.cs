using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebUI.ViewComponents
{
    public class AdminDetailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(){
            User user = HttpContext.Session.GetObject<User>("admin");


            return View(user);
        }
    }
}