namespace Ecommerce.Entity
{
    public class CartItem
    {
        public string CartItemId { get; set; }
        public Cart Cart { get; set; }
        public string CartId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}