using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using TestToDo1.Core.ViewModels;

namespace TestToDo1.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            ShowViewModel<LogInViewModel>();
        }
    }
}
