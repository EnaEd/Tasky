using System;
using System.IO;
using SQLite;
using TestToDo1.Core.Models;

namespace TestToDo1.iOS.Services
{
    public static class SQLConnect_iOS
    {

        public static SQLiteConnection database;
        static SQLConnect_iOS()
        {
            if (database == null)
            {
                database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDatabase_1iOS.db"));
                database.CreateTable<User>();
                database.CreateTable<Item>(); 
            }
        }
    }
}