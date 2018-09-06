using System;
using System.IO;
using SQLite;
using TestToDo1.Core.Models;

namespace TestToDo1.Droid.Services
{
    public static class SQLConnect
    {

        public static SQLiteConnection database;
        static SQLConnect()
        {
                database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDatabase4.db"));
                database.CreateTable<User>();
                database.CreateTable<Item>();
        }
    }
}