using System;
using System.Linq;
using Ecommerce.Data;
using Ecommerce.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptions<MyDbContext> options = new DbContextOptionsBuilder<MyDbContext>().UseSqlite("Data Source=C:\\Users\\alihan\\Desktop\\ecommerce.db").Options;

            MyDbContext myDbContext = new MyDbContext(options);

            Person person = new Person{
                FirstName = "alihan",
                LastName = "bayraktar",
                BirthDate = DateTime.Now,
                Gender = Gender.Man
            };

            Role role  = new Role{
                Name = "Customer"
            };

            User user = new User{
                Name = "alihanbayraktar",
                Password = "test123",
                Role = role,
                Person = person

            };

            User result = myDbContext.Users.Where(u => u.Name == "mervebayraktare" && u.Password == "test123").Include(u => u.Role).FirstOrDefault();

            System.Console.WriteLine(result == null);

            // myDbContext.Users.Add(user);

            // myDbContext.SaveChanges();


        }
    }
}
