using System;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Data;
using Ecommerce.Entity;
using Ecommerce.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebUI.Controllers
{
    public class CartController:Controller
    {
        public MyDbContext _myDbContext;

        public CartController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public IActionResult Index(){
            Cart cart = GetCart();
            return View(cart);
        }


        [HttpPost]
        public IActionResult AddProduct(int productId, int quantity){

            Cart cart = GetCart();

            Product product = _myDbContext.Products.FirstOrDefault(product => product.ProductId == productId);

            cart.AddProduct(product, quantity);

            SetCart(cart);

            return Redirect("/Cart");
        }

        public Cart GetCart(){
            if(HttpContext.Session.GetObject<Cart>("cart") == null){
                Cart cart = new Cart(){CartId = Guid.NewGuid().ToString()};
                SetCart(cart);
            }

            return HttpContext.Session.GetObject<Cart>("cart");
        }

        public void SetCart(Cart cart){
            HttpContext.Session.SetObject("cart", cart);
        }


        public User GetCustomer(){
            return HttpContext.Session.GetObject<User>("customer");
        }

        public void RemoveCart(){
            HttpContext.Session.Remove("cart");
        }
        

        public IActionResult CheckOut(){

            if( GetCustomer() == null)
                return Redirect("/Customer/Login");


            Cart cart = GetCart();

            if(cart.CartItems.Count == 0)
                return Redirect("/Cart");


            User customer = GetCustomer();

            List<CartItem> cartItems = cart.CartItems;
            cart.CartItems = new List<CartItem>();

            foreach(CartItem cartItem in cartItems){
                Product product = _myDbContext.Products.FirstOrDefault(p => p.ProductId == cartItem.Product.ProductId);
                product.StockAmount -= cartItem.Quantity;
                _myDbContext.Products.Update(product);
                cart.AddProduct(product, cartItem.Quantity);
            }

            Order order = new Order
            {
                Cart = cart,
                UserId = customer.UserId,
                Date = DateTime.Now,
                Status = Status.Processing

            };
            
            _myDbContext.Orders.Add(order);

            _myDbContext.SaveChanges();

            RemoveCart();

            return RedirectToAction("Index");
        }
    }
}