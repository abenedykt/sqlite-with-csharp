using System;
using System.Data.Entity;
using System.Linq;

namespace SqLiteTesting
{
	class Program
	{
		static void Main()
		{
			using (var db = new DatabaseContext())
			{
				db.Measurements.Add(new Measurement
				{
					FilePath = "sdf"
				});

				db.SaveChanges();

				Console.WriteLine(db.Measurements.ToList().Count());
			}

		}
	}

	public class DatabaseContext : DbContext
	{
		public DbSet<Measurement> Measurements { get; set; }
	}

	public class Measurement
	{
		public string FilePath { get; set; }

		public string Name { get; set; }

		public DateTime Time { get; set; }

		public bool IsSent { get; set; }
		
	}
}
