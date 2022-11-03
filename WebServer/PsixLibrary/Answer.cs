using System.Collections.Generic;
using System;
using System.Linq;

namespace PsixLibrary
{
	public class Answer
	{

		public int Id { get; set; }

		public string Title { get; set; }
		
		public int Ball { get; set; }
		public virtual List<Prototip> Prototips { get; set; }

		private static ApplicationContext db = Context.db;
		public Answer()
		{
			Prototips = new List<Prototip>();
		}
		public Answer(int id, string title, int ball, List<Prototip> prototips)
		{
			Id = id;
			Title = title;
			Ball = ball;
			Prototips = prototips;

		}
		public static void Add(Answer answer)
		{

		}
	}
}
