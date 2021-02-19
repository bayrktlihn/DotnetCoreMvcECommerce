using Ecommerce.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MyDbContext():base(new DbContextOptionsBuilder<MyDbContext>().UseSqlite("Data Source=C:\\Users\\alihan\\Desktop\\ecommerce.db").Options)
        {
            
        }


        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(p => p.Gender).HasConversion<string>();
            modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();
        }


    }
}