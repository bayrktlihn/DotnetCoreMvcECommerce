using System.Collections.Generic;
using System.Linq;
using Ecommerce.Data;
using Ecommerce.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private MyDbContext _myDbContext;

        public ProductController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public IActionResult List(){
            List<Product> products = _myDbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Detail(int id){
            Product product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);

            return View(product);
        }
    }
}