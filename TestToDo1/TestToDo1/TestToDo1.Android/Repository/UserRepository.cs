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
    public class UserRepository : IUserRepository
    {

        private SQLiteConnection _database;
        public UserRepository()
        {
            _database = SQLConnect.database;
        }

        public IEnumerable<User> Get()
        {
            List<User> result = _database.Table<User>().ToList();
            return result;
        }

        public User GetById(int id)
        {
            User result = _database.Get<User>(id);
            return result;
        }

        public int Delete(int id)
        {
            int result = _database.Delete<User>(id);
            return result;
        }

        public int Save(User item)
        {
            int result;
            if (item.Id != 0)
            {
                _database.Update(item);
                result = item.Id;
                return result;
            }
            result = _database.Insert(item);
            return result;
        }

        public User GetUserByData(string login,string password)
        {
            User result = (_database.Table<User>()
                                .FirstOrDefault(item =>item.UserLogin.Equals(login)&& item.UserPassword.Equals(password)));
            return result;
        }

        public User GetLoggedUser()
        {
            User result= (_database.Table<User>()
                                .FirstOrDefault(item => item.IsLogged.Equals(true)));
            return result;
        }
    }
}