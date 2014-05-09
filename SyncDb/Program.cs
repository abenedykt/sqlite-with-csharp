using System;
using System.Data.SQLite;

namespace SyncDb
{
	class Program
	{
		static void Main(string[] args)
		{
			var storage = new LocalStorage(@"Data Source=files.db;Version=3;");
			storage.AddFile("Hello world");
		}

	}

	public class LocalStorage
	{
		private readonly string _fileName;
		private SQLiteConnection _connection;

		public LocalStorage(string fileName)
		{
			_fileName = fileName;
			_connection = new SQLiteConnection(_fileName);
			_connection.Open();
			var newDatabase = _connection.CreateCommand();
			newDatabase.CommandText = "create table if not exists images (date text, file text, path text, sent integer)";
			newDatabase.ExecuteNonQuery();
			Console.WriteLine("created");

		}

		public void AddFile(string fileName)
		{


			var command = _connection.CreateCommand();
			command.CommandText = string.Format(@"INSERT INTO images values (""{0}"",""{1}"",""{2}"",{3})", fileName, fileName, fileName, 0);
			command.ExecuteNonQuery();
			Console.WriteLine("inserted");
		}
	}

}
