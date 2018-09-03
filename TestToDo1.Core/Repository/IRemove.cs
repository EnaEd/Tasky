using System.Windows.Input;

namespace TestToDo1.Core.Repository
{
    public interface IRemove
    {
        ICommand RemoveCommand { get; }
    }
}
