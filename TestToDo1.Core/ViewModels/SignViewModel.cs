using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Windows.Input;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;

namespace TestToDo1.Core.ViewModels
{
    public class SignViewModel : MvxViewModel
    {
        private readonly IUserRepository _userRepository;
        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(() => Users);
            }
        }

        public static User UserTemp { get; set; }

        private string _userLogin;
        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                RaisePropertyChanged(() => UserLogin);
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                RaisePropertyChanged(() => UserPassword);
            }
        }
        
        private string _error;
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                RaisePropertyChanged(() => Error);
            }
        }

        public SignViewModel(IUserRepository userRepository)
        {
            Error = string.Empty;
            _userRepository = userRepository;
        }

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
            if (!string.IsNullOrEmpty(UserLogin) || !string.IsNullOrEmpty(UserPassword))
            {
                Error = "Fields Login and Password must have a value";
                return;
            }
                UserTemp = new User();
                UserTemp.UserLogin = this.UserLogin;
                UserTemp.UserPassword = this.UserPassword;

                if (_userRepository.GetUserByData(UserLogin)is User)
                {
                    UserTemp = _userRepository.GetUserByData(UserLogin);

                    if (!UserPassword.Equals(UserTemp.UserPassword))
                    {
                        Error = "Wrong password";
                        UserPassword = string.Empty;
                        return;
                    }
                    ShowViewModel<MainViewModel>();
                    return;
                }
                    Error = "UserLogin not exists";
                    UserPassword = string.Empty;
        }

        private MvxCommand _backToCommand;
        public ICommand BackToCommand
        {
            get
            {
                _backToCommand = _backToCommand ?? new MvxCommand(GoBack);
                return _backToCommand;
            }
        }
        private void GoBack()
        {
            ShowViewModel<LogInViewModel>();
        }
    }
}