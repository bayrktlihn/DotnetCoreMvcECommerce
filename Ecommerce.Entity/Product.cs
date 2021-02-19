using System.Collections.Generic;

namespace Ecommerce.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int StockAmount { get; set; }
        public double UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}