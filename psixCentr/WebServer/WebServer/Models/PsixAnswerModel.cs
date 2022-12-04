using PsixLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class PsixAnswerModel
    {
        public int id { get; set; } 
        public AppealModel appeal { get; set; }
        public string answertext { get; set; }
        
        public string date { get; set; }
        public string time { get; set; }

        public PsixAnswerModel()
        {
        }
        public PsixAnswerModel(Answer a)
        {
            id = a.ID;
            appeal = new AppealModel(a.Appeal);
            answertext = a.AnswerText;
            date = a.DateTime.ToString("d");
            time = a.DateTime.ToString("t");
        }
    } 
}
