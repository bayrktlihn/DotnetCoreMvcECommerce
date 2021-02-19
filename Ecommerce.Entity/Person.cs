using System;

namespace Ecommerce.Entity
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public User User { get; set; }
    }

    public enum Gender{
        Man,
        Woman
    }
}