using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class RegistrModel
    {
        public string password { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }

        public static bool Check(RegistrModel? model)
        {
            return model is null || string.IsNullOrEmpty(model.password) ||
            string.IsNullOrEmpty(model.lastname) || string.IsNullOrEmpty(model.firstname) ||
            string.IsNullOrEmpty(model.email);
        }
    }
}
