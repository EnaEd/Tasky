using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestToDo1.Core.Repository
{
    public interface IRemove
    {
        ICommand RemoveCommand { get; }
    }
}
