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
            User user = Mvx.Resolve<IUserRepository>().GetLoggedUser();
            SignViewModel.UserTemp = user;
            if (SignViewModel.UserTemp == null)
            {
                ShowViewModel<LogInViewModel>();
                return;
            }
            if (SignViewModel.UserTemp.IsLogged)
            {
                ShowViewModel<MainViewModel>();
            }
        }
    }
}
