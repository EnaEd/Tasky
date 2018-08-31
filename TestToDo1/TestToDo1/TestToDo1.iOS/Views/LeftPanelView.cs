using System.Drawing;
using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;

namespace TestToDo1.iOS.Views
{
    [Register("LeftPanelView")]
    [MvxPanelPresentation(MvxPanelEnum.Left, MvxPanelHintType.ActivePanel, false)]
    public class LeftPanelView : MvxViewController<LeftPanelViewModel>
    {    
        private UIImageView _userImage;
        private UILabel _userLogin;
        private UIButton _home;
        private UIButton _about;
        private UIButton _logOff;
        private UIView _titleView;
        private UIButton _imageButton;

        public override void ViewDidLoad()
        {            
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            _titleView = new UIView();
            _titleView.BackgroundColor = UIColor.FromRGB(144, 238, 144);
            Add(_titleView);

            _userImage = new UIImageView();
            _userImage.BackgroundColor = UIColor.LightGray;
            Add(_userImage);

            _imageButton=new UIButton();
            _imageButton.BackgroundColor = UIColor.Clear;
            _imageButton.TouchUpInside += ChoosePicture;
            Add(_imageButton);

            _userLogin = new UILabel();
            Add(_userLogin);

            _home = new UIButton();
            _home.SetTitle("Home", UIControlState.Normal);
            _home.SetTitleColor(UIColor.Blue,UIControlState.Normal);
            Add(_home);

            _about = new UIButton();
            _about.SetTitle("About", UIControlState.Normal);
            _about.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            _about.TouchUpInside += TellAboutUs;
            Add(_about);

            _logOff = new UIButton();
            _logOff.SetTitle("LogOff", UIControlState.Normal);
            _logOff.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            Add(_logOff);

            var set = this.CreateBindingSet<LeftPanelView,LeftPanelViewModel>();
            set.Bind(_home).To(vm => vm.HomeCommand);
            set.Bind(_logOff).To(vm => vm.LogOffCommand);
            set.Bind(_userImage).For(v => v.Image).To(vm => vm.UserImage).WithConversion("ByteToUIImage");
            set.Bind(_userLogin).To(vm => vm.UserLogin);
            set.Apply();

        //conastraint
        View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _titleView.WithSameCenterX(View),
                _titleView.WithSameTop(View),
                _titleView.Height().EqualTo(240),
                _titleView.Width().EqualTo(300),

                _userImage.WithSameCenterX(_titleView),
                _userImage.WithSameCenterY(_titleView),
                _userImage.Height().EqualTo(120),
                _userImage.Width().EqualTo(120),

                _imageButton.Height().EqualTo(120),
                _imageButton.Width().EqualTo(120),
                _imageButton.WithSameCenterX(_titleView),
                _imageButton.WithSameCenterY(_titleView),

                _userLogin.Below(_userImage,10),
                _userLogin.WithSameCenterX(_userImage).Minus(40),

                _home.Below(_userLogin,50),
                _home.WithSameLeft(View).Plus(25),

                _about.Below(_home,25),
                _about.WithSameLeft(View).Plus(25),

                _logOff.Below(_about, 25),
                _logOff.WithSameLeft(View).Plus(25)
            );
        }

        private void TellAboutUs(object sender, System.EventArgs e)
        {
            UIAlertController _alertController = UIAlertController.Create("About", "this app ver 2.0", UIAlertControllerStyle.Alert);
            _alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, action =>
                                                        _alertController.Dispose()));
            PresentViewController(_alertController, true, null);
        }

        private void ChoosePicture(object sender, System.EventArgs e)
        {
            UIAlertController _alertController = UIAlertController.Create("Photo from", "Make your choose", UIAlertControllerStyle.Alert);
            _alertController.AddAction(UIAlertAction.Create("Device", UIAlertActionStyle.Default, action =>
                                                        ViewModel.AddPictureFromCamera()));
            _alertController.AddAction(UIAlertAction.Create("Gallery", UIAlertActionStyle.Default, alert =>
                                                        ViewModel.AddPicture()));
            PresentViewController(_alertController, true, null);
        }
    }
}