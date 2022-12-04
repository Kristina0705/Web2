using PsixLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public bool ispsychologist { get; set; }

        public UserModel()
        {
        }
        public UserModel(User u)
        {
            this.id = u.ID;
            this.name = u.Name;
            this.surname = u.Surname;
            this.email = u.Email;
            this.ispsychologist = u.IsPsychologist;
        }
    }
    
}
