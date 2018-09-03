using System.Collections.Generic;
using TestToDo1.Core.Models;

namespace TestToDo1.Core.IRepository
{
    public interface IItemRepository: IItemRepository<Item>
    {
        IEnumerable<Item> Get(int id);
    }
}