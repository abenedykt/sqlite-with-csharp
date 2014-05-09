using System;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;


namespace SyncDb
{
	class Program
	{
		static void Main(string[] args)
		{
OrmLiteConfig.DialectProvider = new SqliteOrmLiteDialectProvider(); 			
var connectionString = "files.db";
			var dbFactory = new OrmLiteConnectionFactory(connectionString, SqliteDialect.Provider, false);
			
				// Wrap all code in using statement to not forget about using db.Close()
			using (var db = dbFactory.Open())
			{
				db.CreateTableIfNotExists<Note>();
				db.Insert(new Note
				{
					Name = "Hello",
					Path = "World"
				});

				var notes = db.Where<Note>("Name", "Hello");
				foreach (var note in notes)
				{
					Console.WriteLine(note.Name);
				}
			}

		}

	}

		

		class Note
	{
		[AutoIncrement]
		// Creates Auto primary key
		public int Id { get; set; }

		public string Name { get; set; }
		public string Path { get; set; }
	}

//	public class LocalStorage
//	{
//		private readonly string _fileName;
//		private SQLiteConnection _connection;
//
//		public LocalStorage(string fileName)
//		{
//			_fileName = fileName;
//			_connection = new SQLiteConnection(_fileName);
//			_connection.Open();
//			var newDatabase = _connection.CreateCommand();
//			newDatabase.CommandText = "create table if not exists images (date text, file text, path text, sent integer)";
//			newDatabase.ExecuteNonQuery();
//			Console.WriteLine("created");
//
//		}
//
//		public void AddFile(string fileName)
//		{
//
//
//			var command = _connection.CreateCommand();
//			command.CommandText = string.Format(@"INSERT INTO images values (""{0}"",""{1}"",""{2}"",{3})", fileName, fileName, fileName, 0);
//			command.ExecuteNonQuery();
//			Console.WriteLine("inserted");
//		}
//	}

}
