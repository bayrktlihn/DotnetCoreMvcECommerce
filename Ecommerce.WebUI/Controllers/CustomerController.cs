using System;
using System.Linq;
using Ecommerce.Data;
using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Ecommerce.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebUI.Controllers
{
    public class CustomerController : Controller
    {

        private MyDbContext _myDbContext;

        public CustomerController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public IActionResult Login(){
            return View(new User());
        }

        [HttpPost]
        public IActionResult Login(User user){
            User foundUser = _myDbContext.Users.Where(u => u.Name == user.Name && u.Password == user.Password).Include(u => u.Role).Where(u => u.Role.Name == "Customer").FirstOrDefault();
            if(foundUser == null){
                ViewBag.ErrorMessage = "Username or password is wrong!";
                return View(new User());
            }

            foundUser.Role.Users = null;


            HttpContext.Session.SetObject("customer", foundUser);
            return Redirect("/Home");
        }

        public IActionResult Logout(){
            HttpContext.Session.Remove("customer");
            return Redirect("/Home");
        }

        public IActionResult Register(){
            ViewBag.GenderOptions = Enum.GetValues(typeof(Gender));
            PersonUserViewModel personUserViewModel = new PersonUserViewModel();
            personUserViewModel.Person = new Person();
            personUserViewModel.User = new User();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult Register(PersonUserViewModel personUserViewModel, string password){

            User user = personUserViewModel.User;
            Person person = personUserViewModel.Person;

            if(_myDbContext.Users.FirstOrDefault(u => u.Name == user.Name) != null){
                TempData["ErrorMessage"] = "Böyle bir kullanıcı adi mevcut";
                return Redirect("/Customer/Register");
            }

            if(personUserViewModel.User.Password != password){
                TempData["ErrorMessage"] = "Parolalar eşleşmiyor. Tekrar deneyiniz";
                return Redirect("/Customer/Register");
            }

            
            user.Person = personUserViewModel.Person;

            user.RoleId = _myDbContext.Roles.FirstOrDefault(r => r.Name == "Customer").RoleId;

            _myDbContext.Users.Add(personUserViewModel.User);

        

            _myDbContext.SaveChanges();

            return Redirect("/Customer/Login");
        }
    }
}