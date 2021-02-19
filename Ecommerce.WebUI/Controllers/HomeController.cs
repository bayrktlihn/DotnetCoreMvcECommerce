using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){
           return Redirect("/Product/List");
        }
    }
}