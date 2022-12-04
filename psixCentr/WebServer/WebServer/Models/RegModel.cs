using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class RegModel
    {
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        
        
        public RegModel(){}
        public static bool Check(RegModel? model)
        {
            return model is null || string.IsNullOrEmpty(model.password) ||
                   string.IsNullOrEmpty(model.surname) || string.IsNullOrEmpty(model.name) ||
                   string.IsNullOrEmpty(model.email);
        }
    }
}
