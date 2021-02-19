using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Entity
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Order Order { get; set; }

        public void AddProduct(Product product, int quantity)
        {

            CartItem cartItem = GetCartItemByProductId(product.ProductId);


            if (cartItem == null)
            {
                CartItems.Add(new CartItem { Quantity = quantity, Product = product, CartItemId = Guid.NewGuid().ToString() });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveCartItemByProductId(int id)
        {
            CartItems.Remove(GetCartItemByProductId(id));
        }

        private CartItem GetCartItemByProductId(int id)
        {
            return CartItems.FirstOrDefault(ci => ci.Product.ProductId == id);
        }


        public double CalculateTotalPrice()
        {
            double result = 0;

            foreach (CartItem cartItem in CartItems)
            {
                result += cartItem.Quantity * cartItem.Product.UnitPrice;
            }

            return result;
        }
    }
}