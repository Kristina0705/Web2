using PsixLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class AppealModel
    {
        public int id { get; set; }
        public string typeappeal { get; set; }
        public UserModel user { get; set; }
        public string text { get; set; }
        public bool isanswered { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        
        public AppealModel(){}

        public AppealModel(Appeal a) {
            this.id = a.ID;
            this.typeappeal = a.TypeAppeal.TypeName;
            this.user = new UserModel(a.User);
            this.text = a.Text;
            this.isanswered = a.IsAnswered;
            this.date = a.DateTime.ToString("d");
            this.time = a.DateTime.ToString("t");
        }

    }
}
