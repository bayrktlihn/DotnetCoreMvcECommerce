using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private MyDbContext _myDbContext;

        
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(MyDbContext myDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _myDbContext = myDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

       


        public IActionResult Login(){
            User user = new User();

            return View(user);
        }

        [HttpPost]
        public IActionResult Login(User user){

            User foundUser = _myDbContext.Users.Where(u => u.Name == user.Name && u.Password == user.Password).Include(u => u.Role).Where(p => p.Role.Name == "Admin").FirstOrDefault();

            if(foundUser == null){
                ViewBag.ErrorMessage = "Username or password is wrong!";
                return View(new User());
            }

            foundUser.Role.Users = null;


            HttpContext.Session.SetObject("admin", foundUser);
            return RedirectToAction("Index");
        }

        public IActionResult Index(){
            if(HttpContext.Session.GetObject<User>("admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Logout(){
            
            HttpContext.Session.Remove("admin");
            return RedirectToAction("Login");
        }

        public IActionResult DeleteProduct(int id){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            _myDbContext.Products.Remove(new Product{ProductId = id});
            _myDbContext.SaveChanges();

            return Redirect("/Admin/ListProduct");
        }

        public IActionResult EditProduct(int id){
            // if(HttpContext.Session.GetObject<User>("admin") == null){
            //     return RedirectToAction("Login");
            // }

            Product product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            ViewBag.Categories = _myDbContext.Categories.ToList();

            return View(product);
        }

        public IActionResult AddProduct(){
            Product product = new Product();
            ViewBag.Categories = _myDbContext.Categories.ToList();

            return View(product);
        }

        


        public IActionResult ListProduct(){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            List<Product> products = _myDbContext.Products.Include(p => p.Category).ToList();
            
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(Product product, IFormFile imageFile){
            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            if(product.ProductId == 0){
                _myDbContext.Products.Add(product);
            }
            else{
                _myDbContext.Products.Update(product);
            }

            System.Console.WriteLine(imageFile == null);
            System.Console.WriteLine(product.ImageUrl);

            if(imageFile != null){
                string imageName =  imageFile.FileName;
                
                string newImageName = Guid.NewGuid().ToString();
                newImageName += imageName;

                string fullPath = _webHostEnvironment.WebRootPath + "/img/product/" +newImageName;
                using(FileStream fs = System.IO.File.Create(fullPath))
                {
                    await imageFile.CopyToAsync(fs);
                }

                product.ImageUrl = newImageName;
            }

            _myDbContext.SaveChanges();

            return Redirect("/Admin/ListProduct");
        }

        public IActionResult AddCategory(){
            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }
            Category category = new Category();
            return View(category);
        }

        [HttpPost]
        public IActionResult SaveCategory(Category category){
            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            if(category.CategoryId == 0){
                _myDbContext.Categories.Add(category);
            }
            else{
                _myDbContext.Categories.Update(category);
            }
            _myDbContext.SaveChanges();

            return RedirectToAction("ListCategory");
        }

        public IActionResult ListCategory(){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            List<Category> categories = _myDbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult DeleteCategory(int id){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            _myDbContext.Categories.Remove(new Category{CategoryId = id});
            _myDbContext.SaveChanges();

            return Redirect("/Admin/ListCategory");
        }

        public IActionResult EditCategory(int id){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            Category category = _myDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            return View(category);
        }

        public IActionResult ListOrder(){
            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            List<Order> orders = _myDbContext.Orders.Include(o => o.Cart).ThenInclude(c => c.CartItems).ThenInclude(ci => ci.Product).ToList();
            return View(orders);
        }

        public IActionResult OrderDetail(int id){

            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            Order order = _myDbContext.Orders.Where(o => o.OrderId == id).Include(o => o.Cart).ThenInclude(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefault();
            return View(order);
        }

        public IActionResult DeleteOrder(int id){
            if(HttpContext.Session.GetObject<User>("admin") == null){
                return RedirectToAction("Login");
            }

            _myDbContext.Orders.Remove(new Order{OrderId = id});

            _myDbContext.SaveChanges();

            return Redirect("/Admin/ListOrder");

        }

        
    }
}