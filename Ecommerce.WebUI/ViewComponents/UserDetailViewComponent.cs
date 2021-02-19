using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebUI.ViewComponents
{
    public class UserDetailViewComponent : ViewComponent
    {



        public IViewComponentResult Invoke(){
            User user = HttpContext.Session.GetObject<User>("customer");


            return View(user);
        }
    }
}