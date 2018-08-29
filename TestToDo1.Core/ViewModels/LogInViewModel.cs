using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.PictureChooser;

namespace TestToDo1.Core.ViewModels
{
    public class LogInViewModel : MvxViewModel
    {
        private MvxCommand _signCommand;
        public ICommand SignCommand
        {
            get
            {
                return _signCommand = _signCommand ?? new MvxCommand(DoSign);
            }
        }
        private void DoSign()
        {
            ShowViewModel<SignViewModel>();
        }

        private MvxCommand _registrationCommand;
        public ICommand RegistrationCommand
        {
            get
            {
                return _registrationCommand = _registrationCommand ?? new MvxCommand(DoRegistration);
            }
        }
        private void DoRegistration()
        {
            ShowViewModel<RegistrationViewModel>();
        }
    }     
}