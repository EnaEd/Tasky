using System.Collections.Generic;
using TestToDo1.Core.Models;

namespace TestToDo1.Core.IRepository
{
    public interface IItemRepository<T> where T : AbstractModel
    {
        IEnumerable<T> Get();
        T GetById(int id);
        int Delete(int id);
        int Save(T item);
    }
}
