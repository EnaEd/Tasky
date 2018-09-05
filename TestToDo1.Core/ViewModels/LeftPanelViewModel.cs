using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using MvvmCross.Plugins.PictureChooser;
using System.IO;
using MvvmCross.Platform;

namespace TestToDo1.Core.ViewModels
{
    //iOS drawer panel
    public class LeftPanelViewModel : MvxViewModel
    {

        private  IMvxPictureChooserTask _pictureChooserTask;

        private int _maxPixel;
        private int _qualityPercent;

        private byte[] _userImage;
        public byte[] UserImage { get { return _userImage; }
            set
            {
                _userImage = value;
                RaisePropertyChanged(() => UserImage);
            }
        }

        public string UserLogin { get; set; }
        
        public LeftPanelViewModel(IMvxPictureChooserTask pictureChooserTask)
        {
                //for photo on drawerLayout
                UserImage = SignViewModel.UserCurrent.UserImage;
                UserLogin = SignViewModel.UserCurrent.UserLogin;

            _pictureChooserTask = pictureChooserTask;
            _maxPixel = 400;
            _qualityPercent = 90;
        }

        private MvxCommand _homeCommand;
        public ICommand HomeCommand
        {
            get
            {
                return _homeCommand = _homeCommand ?? new MvxCommand(GoHome);
            }
        }
        public void GoHome()
        {
            ShowViewModel<MainViewModel>();
        }

        private MvxCommand _logOffCommand;
        public ICommand LogOffCommand
        {
            get
            {
                return _logOffCommand = _logOffCommand ?? new MvxCommand(DoLogOff);
            }
        }
        public void DoLogOff()
        {
            ShowViewModel<LogInViewModel>();
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
        public ICommand AddPictureCameraCommand
        {
            get
            {
                _addPictureCamera = _addPictureCamera ?? new MvxCommand(AddPictureFromCamera);
                return _addPicture;
            }

        }
        public void AddPictureFromCamera()
        {
            //for camera first
            _pictureChooserTask.TakePicture(_maxPixel, _qualityPercent, OnPicture, () => { });
        }

        private void OnPicture(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            UserImage = memoryStream.ToArray();
            SignViewModel.UserCurrent.UserImage = UserImage;
        }

    }
}