using System.Drawing;
using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using System.Linq;

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
        private UIView _contentConteiner;
        private UIScrollView _scrollView;

        public override void ViewDidLoad()
        {            
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            _contentConteiner = new UIView();
            _scrollView = new UIScrollView();
            _scrollView.AddSubview(_contentConteiner);
            Add(_scrollView);

            _titleView = new UIView();
            _titleView.BackgroundColor = UIColor.FromRGB(144, 238, 144);
            _contentConteiner.AddSubview(_titleView);

            _userImage = new UIImageView();
            _userImage.BackgroundColor = UIColor.LightGray;
            _contentConteiner.AddSubview(_userImage);

            _imageButton =new UIButton();
            _imageButton.BackgroundColor = UIColor.Clear;
            _imageButton.TouchUpInside += ChoosePicture;
            _contentConteiner.AddSubview(_imageButton);

            _userLogin = new UILabel();
            _contentConteiner.AddSubview(_userLogin);

            _home = new UIButton();
            _home.SetTitle("Home", UIControlState.Normal);
            _home.SetTitleColor(UIColor.Blue,UIControlState.Normal);
            _contentConteiner.AddSubview(_home);

            _about = new UIButton();
            _about.SetTitle("About", UIControlState.Normal);
            _about.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            _contentConteiner.AddSubview(_about);

            _logOff = new UIButton();
            _logOff.SetTitle("LogOff", UIControlState.Normal);
            _logOff.SetTitleColor(UIColor.Blue, UIControlState.Normal);
            _contentConteiner.AddSubview(_logOff);

            var set = this.CreateBindingSet<LeftPanelView,LeftPanelViewModel>();
            set.Bind(_home).To(vm => vm.HomeCommand);
            set.Bind(_logOff).To(vm => vm.LogOffCommand);
            set.Bind(_userImage).For(v => v.Image).To(vm => vm.UserImage).WithConversion("ByteToUIImage");
            set.Bind(_userLogin).To(vm => vm.UserLogin);
            set.Apply();

            //conastraint
            _scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            _scrollView.AddConstraints(_contentConteiner.FullWidthOf(_scrollView));
            _scrollView.AddConstraints(_contentConteiner.FullHeightOf(_scrollView));

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(_scrollView.FullWidthOf(View));
            View.AddConstraints(_scrollView.FullHeightOf(View));
            View.AddConstraints(
                _contentConteiner.WithSameWidth(View),
                _contentConteiner.WithSameHeight(View).SetPriority(UILayoutPriority.DefaultLow)
            );

            _contentConteiner.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            _contentConteiner.AddConstraints(
                _titleView.WithSameCenterX(_contentConteiner),
                _titleView.AtTopOf(_contentConteiner),
                _titleView.Height().EqualTo(240),
                _titleView.Width().EqualTo(300),

                _userImage.WithSameCenterX(_titleView),
                _userImage.WithSameCenterY(_titleView),
                _userImage.Height().EqualTo(120),
                _userImage.Width().EqualTo(120),

                _imageButton.Height().EqualTo(120),
                _imageButton.Width().EqualTo(120),
                _imageButton.WithSameCenterX(_userImage),
                _imageButton.WithSameCenterY(_userImage),

                _userLogin.Below(_userImage,10),
                _userLogin.WithSameWidth(_contentConteiner).Minus(40),

                _home.Below(_userLogin,50),
                _home.AtLeftOf(_contentConteiner,25),

                _about.Below(_home,25),
                _about.AtLeftOf(_contentConteiner, 25),

                _logOff.Below(_about, 25),
                _logOff.AtLeftOf(_contentConteiner, 25)
            );
            // very important to make scrolling work
            var bottomViewConstraint = _contentConteiner.Subviews.Last()
                .AtBottomOf(_contentConteiner).Minus(20);
            _contentConteiner.AddConstraints(bottomViewConstraint);
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