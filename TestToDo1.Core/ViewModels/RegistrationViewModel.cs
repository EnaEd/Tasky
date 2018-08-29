using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;
using System.Text.RegularExpressions;

namespace TestToDo1.Core.ViewModels
{
    public class RegistrationViewModel : MvxViewModel
    {

        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IUserRepository _userRepository;
        private string _passwordPattern;

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

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

        private string _userPasswordRepeat;
        public string UserPasswordRepeat
        {
            get
            {
                return _userPasswordRepeat;
            }
            set
            {
                _userPasswordRepeat = value;
                RaisePropertyChanged(() => UserPasswordRepeat);
            }
        }

        private byte[] _userImage;
        public byte[] UserImage
        {
            get { return _userImage; }
            set
            {
                _userImage = value;
                RaisePropertyChanged(() => UserImage);

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

        private int _maxPixel;
        private int _qualityPercent;

        public RegistrationViewModel(IMvxPictureChooserTask pictureChooserTask, IUserRepository userRepository)
        {
            _pictureChooserTask = pictureChooserTask;
            _passwordPattern = @"(?=.*[0-9])(?=.*\w)(?=.*[A-Z])[0-9\wA-Z]{6,}";
            _maxPixel = 400;
            _qualityPercent = 90;
            _userRepository = userRepository;
        }

        private bool PasswordCheck()
        {
            return (_userPassword.Equals(_userPasswordRepeat));
        }

        private bool PasswordFormatCheck()
        {
            return (Regex.IsMatch(UserPassword, _passwordPattern));
        }

        private MvxCommand _creareUserCommand;
        public ICommand CreateUserCommand
        {
            get
            {
                _creareUserCommand = _creareUserCommand ?? new MvxCommand(DoCreateUser);
                return _creareUserCommand;
            }
        }
        private void DoCreateUser()
        {
            bool checkFormat = PasswordFormatCheck();
            if (!checkFormat)
            {
                Error = "Wrong password format";
                UserPassword = String.Empty;
                UserPasswordRepeat = String.Empty;
                return;
            }
            bool checkEqualsPassword = PasswordCheck();
            if (!checkEqualsPassword)
            {
                Error = "Passwords not equals ";
                UserPassword = String.Empty;
                UserPasswordRepeat = String.Empty;
                return;
            }

            if (!string.IsNullOrEmpty(UserLogin))
            {
                SignViewModel.UserTemp = new User();
                SignViewModel.UserTemp.UserLogin = this.UserLogin;
                SignViewModel.UserTemp.UserPassword = this.UserPassword;
                SignViewModel.UserTemp.UserImage = UserImage;
                if (_userRepository.GetUserByData(UserLogin) is User)
                {
                    Error = "This user exists";
                    UserPassword = String.Empty;
                    UserPasswordRepeat = String.Empty;
                    return;
                }
                  _userRepository.Save(SignViewModel.UserTemp);
                  ShowViewModel<MainViewModel>();
                    return;
            }
        }

        private MvxCommand _addPicture;
        public ICommand AddPictureCommand
        {
            get
            {
                _addPicture = _addPicture ?? new MvxCommand(AddPicture);
                return _addPicture;
            }
        }
        public void AddPicture()
        {
            
            _pictureChooserTask.ChoosePictureFromLibrary(_maxPixel, _qualityPercent, OnPicture, () => { });
        }

        private MvxCommand _addPictureCamera;
        public ICommand AddPictureCamera
        {
            get
            {
                _addPictureCamera = _addPictureCamera ?? new MvxCommand(DoAddPictureFromCamera);
                return _addPicture;
            }

        }
        public void DoAddPictureFromCamera()
        {
            //for camera first
            _pictureChooserTask.TakePicture(_maxPixel, _qualityPercent, OnPicture, () => { });
        }

        private void OnPicture(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            UserImage = memoryStream.ToArray();
        }


    }
}