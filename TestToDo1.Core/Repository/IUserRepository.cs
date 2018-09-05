using TestToDo1.Core.Models;

namespace TestToDo1.Core.IRepository
{
    public interface IUserRepository: IItemRepository<User>
    {
        User GetUserByData(string login,string password);
    }
}