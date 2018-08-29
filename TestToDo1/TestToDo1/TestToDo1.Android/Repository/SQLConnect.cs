using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SQLite;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;

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