using System;
using System.IO;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
namespace SyncDb
{
	class Program
	{
		static void Main(string[] args)
		{
			const string connectionString = "sync.db";

			OrmLiteConfig.DialectProvider = new SqliteOrmLiteDialectProvider();
			var dbFactory = new OrmLiteConnectionFactory(connectionString, SqliteDialect.Provider, false);
			
			// Wrap all code in using statement to not forget about using db.Close()
			using (var db = dbFactory.Open())
			{
				db.CreateTableIfNotExists<File>();

				var path = new DirectoryInfo(args[0]);
				
				Console.WriteLine("path : {0}", path);
				foreach (var file in path.GetFiles())
				{
					db.Insert(new File
					{
						Name = file.Name,
						Path = file.FullName
					});	
				}
				

				var notes = db.Select<File>();
				foreach (var note in notes)
				{
					Console.WriteLine(note.Name);
				}

				db.Close();
			}
		}
	}

	public class File
	{
		[AutoIncrement]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Path { get; set; }
	}
}
