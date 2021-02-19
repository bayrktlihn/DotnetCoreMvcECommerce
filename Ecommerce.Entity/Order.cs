using System;

namespace Ecommerce.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public Cart Cart { get; set; }
        public string CartId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
    }

    public enum Status{
        Shipped,
        Processing,
        Delivered,
        Cancelled
    }
}