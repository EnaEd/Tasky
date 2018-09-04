using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using System.Drawing;
using System.Linq;

namespace TestToDo1.iOS.Views
{
    [Register("RegistrationView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class RegistrationView : MvxViewController<RegistrationViewModel>
    {
        private UIButton _buttonCreate;
        private UITextField _textUserName;
        private UITextField _textUserPassword;
        private UITextField _textUserPasswordRepeat;
        private UIImageView _imageUserPhoto;
        private UIButton _buttonPhoto;
        private UILabel _labelError;
        private UIScrollView _scrollView;
        private UIView _contentConteiner;
        private UILabel _passwordPattern;

        public RegistrationView()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            _contentConteiner = new UIView();
            _scrollView = new UIScrollView();
            _scrollView.AddSubview(_contentConteiner);
            Add(_scrollView);


            _scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            _scrollView.AddConstraints(_contentConteiner.FullWidthOf(_scrollView));
            _scrollView.AddConstraints(_contentConteiner.FullHeightOf(_scrollView));

            var _BackBarButton = new UIBarButtonItem();
            _BackBarButton.Title = string.Empty;
            NavigationItem.RightBarButtonItem = _BackBarButton;
            var _MenuBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "Back";
            NavigationItem.LeftBarButtonItem = _BackBarButton;


            _labelError = new UILabel();
            _labelError.TextColor = UIColor.Red;
            _labelError.Font=_labelError.Font.WithSize(10);
            _contentConteiner.AddSubview(_labelError);

            _textUserName = new UITextField();
            _textUserName.Placeholder = "Login";
            _textUserName.Layer.CornerRadius = 5;
            _textUserName.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textUserName);

            _textUserPassword = new UITextField();
            _textUserPassword.Placeholder = "Password";
            _textUserPassword.Layer.CornerRadius = 5;
            _textUserPassword.SecureTextEntry = true;
            _textUserPassword.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textUserPassword);

            _textUserPasswordRepeat = new UITextField();
            _textUserPasswordRepeat.Placeholder = "Password repeat";
            _textUserPasswordRepeat.Layer.CornerRadius = 5;
            _textUserPasswordRepeat.SecureTextEntry = true;
            _textUserPasswordRepeat.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textUserPasswordRepeat);

            _passwordPattern = new UILabel();
            _passwordPattern.Text = "password must contain at least 6 characters, min 1 letter UpperCase,min 1 digit";
            _passwordPattern.Font = _passwordPattern.Font.WithSize(10);
            _passwordPattern.LineBreakMode = UILineBreakMode.WordWrap;
            _passwordPattern.Lines = 0;
            _contentConteiner.AddSubview(_passwordPattern);

            _imageUserPhoto = new UIImageView();
            _imageUserPhoto.Layer.CornerRadius = this._imageUserPhoto.Frame.Size.Height / 2;
            _imageUserPhoto.ClipsToBounds = true;
            _imageUserPhoto.BackgroundColor = UIColor.LightGray;
            _contentConteiner.AddSubview(_imageUserPhoto);

            _buttonPhoto = new UIButton();
            _buttonPhoto.Layer.CornerRadius = this._buttonPhoto.Frame.Size.Height / 2;
            _buttonPhoto.ClipsToBounds = true;
            _buttonPhoto.BackgroundColor = UIColor.Clear;
            _buttonPhoto.TouchUpInside += ChoosePicture;
            _contentConteiner.AddSubview(_buttonPhoto);

            _buttonCreate = new UIButton(UIButtonType.RoundedRect);
            _buttonCreate.SetTitle("Create", UIControlState.Normal);
            _buttonCreate.Layer.CornerRadius = 5;
            _buttonCreate.BackgroundColor = UIColor.Blue;
            _buttonCreate.SetTitleColor(UIColor.White, UIControlState.Normal);
            _contentConteiner.AddSubview(_buttonCreate);

            var set = this.CreateBindingSet<RegistrationView, RegistrationViewModel>();
            set.Bind(_textUserName).To(vm => vm.UserLogin);
            set.Bind(_labelError).To(vm => vm.Error);
            set.Bind(_textUserPassword).To(vm => vm.UserPassword);
            set.Bind(_textUserPasswordRepeat).To(vm => vm.UserPasswordRepeat);
            set.Bind(_buttonCreate).To(vm => vm.CreateUserCommand);
            set.Bind(_imageUserPhoto).For(v=>v.Image).To(vm => vm.UserImage).WithConversion("ByteToUIImage");
            set.Bind(_BackBarButton).To(vm => vm.BackToCommand);
            set.Apply();

            //conastraint
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(_scrollView.FullWidthOf(View));
            View.AddConstraints(_scrollView.FullHeightOf(View));
            View.AddConstraints(
                _contentConteiner.WithSameWidth(View),
                _contentConteiner.WithSameHeight(View).SetPriority(UILayoutPriority.DefaultLow)
            );

            _contentConteiner.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            _contentConteiner.AddConstraints(

                _labelError.WithSameCenterX(_contentConteiner),
                //_labelError.WithSameWidth(_contentConteiner).Minus(100),
                _labelError.AtTopOf(_contentConteiner),
                _labelError.Height().EqualTo(13),

                _textUserName.AtLeftOf(_contentConteiner,25),
                _textUserName.WithSameWidth(_contentConteiner).Minus(100),
                _textUserName.Below(_labelError,60),

                _textUserPassword.AtLeftOf(_contentConteiner, 25),
                _textUserPassword.WithSameWidth(_contentConteiner).Minus(130),
                _textUserPassword.Below(_textUserName, 40),

                 _textUserPasswordRepeat.AtLeftOf(_contentConteiner, 25),
                _textUserPasswordRepeat.WithSameWidth(_contentConteiner).Minus(130),
                _textUserPasswordRepeat.Below(_textUserPassword, 40),

                _imageUserPhoto.AtRightOf(_contentConteiner,25),
                _imageUserPhoto.WithSameCenterY(_textUserName),
                _imageUserPhoto.Width().EqualTo(80),
                _imageUserPhoto.Height().EqualTo(80),

                _buttonPhoto.WithSameCenterX(_imageUserPhoto),
                _buttonPhoto.Width().EqualTo(80),
                _buttonPhoto.Height().EqualTo(80),
                _buttonPhoto.Below(_imageUserPhoto,-80)
                );
            View.AddConstraints(_passwordPattern.Below(_textUserPasswordRepeat, 2));
            View.AddConstraints(_passwordPattern.FullWidthOf(View, 25));

            View.AddConstraints(_buttonCreate.FullWidthOf(View, 25));
            View.AddConstraints(_buttonCreate.Below(_passwordPattern, 25));
            View.AddConstraints(_buttonCreate.Height().LessThanOrEqualTo(35));

            // very important to make scrolling work
            var bottomViewConstraint = _contentConteiner.Subviews.Last()
               .AtBottomOf(_contentConteiner).Minus(20);
            _contentConteiner.AddConstraints(bottomViewConstraint);

            //disable swipe
            CreateGestureRecognizer();
        }
        void CreateGestureRecognizer()
        {
            UIPanGestureRecognizer swipeRecognizer = new UIPanGestureRecognizer();
            swipeRecognizer.AddTarget(() => HandleSwipe(swipeRecognizer));
            View.AddGestureRecognizer(swipeRecognizer);
        }

        void HandleSwipe(UIPanGestureRecognizer sender)
        {
        }

        private void ChoosePicture(object sender, System.EventArgs e)
        {
            UIAlertController _alertController = UIAlertController.Create("Photo from", "Make your choose", UIAlertControllerStyle.Alert);
            _alertController.AddAction(UIAlertAction.Create("Device", UIAlertActionStyle.Default, action =>
                                                        ViewModel.DoAddPictureFromCamera()));
            _alertController.AddAction(UIAlertAction.Create("Gallery", UIAlertActionStyle.Default, alert =>
                                                        ViewModel.AddPicture()));
            PresentViewController(_alertController, true, null);
        }
    }
}