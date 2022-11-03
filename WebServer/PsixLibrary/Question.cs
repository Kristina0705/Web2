using System.Collections.Generic;
using System;
using System.Linq;


namespace PsixLibrary
{
	public class Question
	{
		private static ApplicationContext db = Context.db;
		public int Id { get; set; }
		public string Vopros { get; set; }
		public virtual List<Prototip> Prototips { get; set; }

		public Question()
		{
			Prototips = new List<Prototip>();
		}

		public Question(string vopros, List<Prototip> prototips)
        {
            Vopros = vopros;
			Prototips = prototips;
		}

		public static void Add(Question question)
		{
			db.Question.Add(question);
			db.SaveChanges();
		}

        public static List<Question> GetOrderViewsUpcoming()
        {
			return db.Question.ToList();
        }
    }
}
