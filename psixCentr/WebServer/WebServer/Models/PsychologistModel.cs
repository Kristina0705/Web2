using PsixLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class PsychologistModel
    {
        public int id { get; set; }
        public UserModel user { get; set; }
        public  string name { get; set; }
        public  string surname { get; set; }
        public  string? middlename { get; set; }
        public List<PsixAnswerModel> answerlist { get; set; } = new List<PsixAnswerModel>();
        
        PsychologistModel(){}
      
        
        public PsychologistModel(Psychologist p) {
            id = p.ID;
            user = new UserModel(p.User);
            name = p.Name;
            surname = p.Surname;
            middlename = p.MiddleName;
            foreach (var a in p.AnswerList)
            {
                answerlist.Add(new PsixAnswerModel(a));
            }
        }
    }
}
