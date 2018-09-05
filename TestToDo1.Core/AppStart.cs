using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using TestToDo1.Core.IRepository;
using TestToDo1.Core.Models;
using TestToDo1.Core.ViewModels;



namespace TestToDo1.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            if (SignViewModel.UserTemp is User)
            {
                ShowViewModel<MainViewModel>();
                return;
            }
            ShowViewModel<LogInViewModel>();
        }
    }
}
