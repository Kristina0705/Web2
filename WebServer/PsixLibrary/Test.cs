using System.Collections.Generic;
using System;
using System.Linq;

namespace PsixLibrary
{
	public class Test
	{
		private static ApplicationContext db = Context.db;
		public int Id { get; set; }
		public string Name { get; set; }
		public string Opisanie { get; set; }

		public virtual List<Prototip> Prototips { get; set; }
		

		public Test()
		{
			Prototips = new List<Prototip>();
		}
		public Test(string name, string opisanie, List<Prototip> prototips)
        {
            Name = name;
            Opisanie = opisanie;
            Prototips = prototips;
        }

		public static void Add(Test test)
        {
			db.Test.Add(test);
			db.SaveChanges();
        }


	}
}
