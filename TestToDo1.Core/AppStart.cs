using MvvmCross.Core.ViewModels;
using TestToDo1.Core.ViewModels;



namespace TestToDo1.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            if (SignViewModel.UserCurrent != null)
            {
                ShowViewModel<MainViewModel>();
                return;
            }
            ShowViewModel<LogInViewModel>();
        }
    }
}
