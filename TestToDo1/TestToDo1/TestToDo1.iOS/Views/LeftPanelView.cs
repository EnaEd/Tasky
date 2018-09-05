using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using System.Linq;
using System.IO;
using MvvmCross.Platform;

namespace TestToDo1.iOS.Views
{
    [Register("LeftPanelView")]
    [MvxPanelPresentation(MvxPanelEnum.Left, MvxPanelHintType.ActivePanel, false)]
    public class LeftPanelView : MvxViewController<LeftPanelViewModel>
    {    
        private UIImageView _userImage;
        private UILabel _userLogin;
        private UIButton _home;
        private UIButton _logOff;
        private UIView _titleView;
        private UIButton _imageButton;
        private UIView _contentConteiner;
        private UIScrollView _scrollView;
        private string _filePath;

        public override void ViewDidLoad()
        {            
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromRGB(0, 153, 204);

            //save user
            _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoUser.txt");
            File.WriteAllText(_filePath, $"{SignViewModel.UserCurrent.UserLogin}`" +
                                        $"{SignViewModel.UserCurrent.UserPassword}");

            _contentConteiner = new UIView();
            _scrollView = new UIScrollView();
            _scrollView.AddSubview(_contentConteiner);
            Add(_scrollView);

            _titleView = new UIView();
            _titleView.BackgroundColor = UIColor.FromRGB(245, 245, 239);
            _titleView.Layer.CornerRadius = 10;
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
            _home.SetTitle("Task List", UIControlState.Normal);
            _home.SetTitleColor(UIColor.Black,UIControlState.Normal);
            _home.SetImage(UIImage.FromFile("Image/todoIOS.png"), UIControlState.Normal);
            _home.TouchUpInside += delegate { File.Delete(_filePath);
                                              ViewModel.GoHome();
                                              Mvx.Resolve<IMvxSideMenu>().Close();
                                            };
            _contentConteiner.AddSubview(_home);

            _logOff = new UIButton();
            _logOff.SetTitle("LogOff", UIControlState.Normal);
            _logOff.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _logOff.SetImage(UIImage.FromFile("Image/logoffIOS.png"), UIControlState.Normal);
            _logOff.TouchUpInside += delegate{  File.Delete(_filePath);
                                                ViewModel.DoLogOff();
                                                Mvx.Resolve<IMvxSideMenu>().Close();
            };
            _contentConteiner.AddSubview(_logOff);

            var set = this.CreateBindingSet<LeftPanelView,LeftPanelViewModel>();
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
                _titleView.AtTopOf(_contentConteiner).Plus(20),
                _titleView.Height().EqualTo(250),
                _titleView.WithSameWidth(_contentConteiner).Minus(10),

                _userImage.WithSameCenterX(_titleView),
                _userImage.WithSameCenterY(_titleView),
                _userImage.Height().EqualTo(120),
                _userImage.Width().EqualTo(120),

                _imageButton.Height().EqualTo(120),
                _imageButton.Width().EqualTo(120),
                _imageButton.WithSameCenterX(_userImage),
                _imageButton.WithSameCenterY(_userImage),

                _userLogin.Below(_userImage,10),
                _userLogin.WithSameCenterX(_userImage),

                _home.Below(_userLogin,50),
                _home.AtLeftOf(_contentConteiner,5),

                _logOff.Below(_home, 5),
                _logOff.AtLeftOf(_contentConteiner,5)
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