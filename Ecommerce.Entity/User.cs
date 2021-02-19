namespace Ecommerce.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public string ImageUrl { get; set; }
    }
}