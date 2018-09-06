using System.Collections.Generic;
using System.Linq;
using SQLite;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;

namespace TestToDo1.Droid.Services
{
    public class ItemRepository:IItemRepository
    {

        public SQLiteConnection _database { get; set; }
        public ItemRepository()
        {
            _database = SQLConnect.database;
        }

        public IEnumerable<Item> Get()
        {
            List<Item> result = _database.Table<Item>().ToList();
            return result;
        }

        public Item GetById(int id)
        {
            Item result = _database.Get<Item>(id);
            return result;
        }

        public int Delete(int id)
        {
            int result = _database.Delete<Item>(id);
            return result;
        }

        public int Save(Item item)
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
        
        public IEnumerable<Item> Get(int id)
        {
            List<Item> items = (_database.Table<Item>()
                                .Where(item => item.UserId.Equals(id))).ToList();
            return items;
        }
    }
}