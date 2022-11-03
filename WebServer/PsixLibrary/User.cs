using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsixLibrary
{
    public class User
    {
        private static ApplicationContext db = Context.db;
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string LastName, string Name, string Email, string Password)
        {
            this.LastName = LastName;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }
 

        public static User? GetClientByEmail(string Email) => db.User.FirstOrDefault(x => x.Email == Email);

        public static User? GetClientAuth(string Email, string Password)
        {
            return db.User.FirstOrDefault(x => x.Email == Email && x.Password == Password);
        }

        public static void Add(User client)
        {
            db.User.Add(client);
            db.SaveChanges();
        }
    }
}
