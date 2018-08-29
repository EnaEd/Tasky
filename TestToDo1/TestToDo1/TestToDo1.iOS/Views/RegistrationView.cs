using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;

namespace TestToDo1.iOS.Views
{
    [Register("RegistrationView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class RegistrationView : MvxViewController<RegistrationViewModel>
    {
        private UIToolbar _uIToolbar;
        private UIButton _buttonCreate;
        private UITextField _textUserName;
        private UITextField _textUserPassword;
        private UITextField _textUserPasswordRepeat;
        private UIImageView _imageUserPhoto;
        private UIButton _buttonPhoto;
        private UILabel _labelError;

        public RegistrationView()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            var _BackBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "";
            NavigationItem.RightBarButtonItem = _BackBarButton;
            var _MenuBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "";
            NavigationItem.LeftBarButtonItem = _BackBarButton;

            _uIToolbar = new UIToolbar();
            _uIToolbar.BackgroundColor = UIColor.LightGray;
            Add(_uIToolbar);

            _labelError = new UILabel();
            _labelError.TextColor = UIColor.Red;
            Add(_labelError);

            _textUserName = new UITextField();
            _textUserName.Placeholder = "Login";
            _textUserName.Layer.CornerRadius = 5;
            Add(_textUserName);

            _textUserPassword = new UITextField();
            _textUserPassword.Placeholder = "Password";
            _textUserPassword.Layer.CornerRadius = 5;
            _textUserPassword.SecureTextEntry = true;
            Add(_textUserPassword);

            _textUserPasswordRepeat = new UITextField();
            _textUserPasswordRepeat.Placeholder = "Password repeat";
            _textUserPasswordRepeat.Layer.CornerRadius = 5;
            _textUserPasswordRepeat.SecureTextEntry = true;
            Add(_textUserPasswordRepeat);

            _imageUserPhoto = new UIImageView();
            _imageUserPhoto.Layer.CornerRadius = this._imageUserPhoto.Frame.Size.Height / 2;
            _imageUserPhoto.ClipsToBounds = true;
            _imageUserPhoto.BackgroundColor = UIColor.LightGray;
            Add(_imageUserPhoto);

            _buttonPhoto = new UIButton();
            _buttonPhoto.Layer.CornerRadius = this._buttonPhoto.Frame.Size.Height / 2;
            _buttonPhoto.ClipsToBounds = true;
            _buttonPhoto.BackgroundColor = UIColor.Clear;
            _buttonPhoto.TouchUpInside += ChoosePicture;
            
            Add(_buttonPhoto);

            _buttonCreate = new UIButton(UIButtonType.RoundedRect);
            _buttonCreate.SetTitle("Create", UIControlState.Normal);
            _buttonCreate.Layer.CornerRadius = 5;
            _buttonCreate.BackgroundColor = UIColor.Blue;
            _buttonCreate.SetTitleColor(UIColor.White, UIControlState.Normal);
            Add(_buttonCreate);

            var set = this.CreateBindingSet<RegistrationView, RegistrationViewModel>();
            set.Bind(_textUserName).To(vm => vm.UserLogin);
            set.Bind(_labelError).To(vm => vm.Error);
            set.Bind(_textUserPassword).To(vm => vm.UserPassword);
            set.Bind(_textUserPasswordRepeat).To(vm => vm.UserPasswordRepeat);
            set.Bind(_buttonCreate).To(vm => vm.CreateUserCommand);
            set.Bind(_imageUserPhoto).For(v=>v.Image).To(vm => vm.UserImage).WithConversion("ByteToUIImage");
            set.Apply();

            //conastraint
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _uIToolbar.WithSameCenterX(View),
                _uIToolbar.WithSameTop(View).Plus(60),
                _uIToolbar.Width().EqualTo(View.Frame.Width).Minus(0),

                _labelError.WithSameCenterX(View).Plus(20),
                _labelError.Width().EqualTo(View.Frame.Width).Minus(100),
                _labelError.Below(_uIToolbar, -30),

                _textUserName.WithSameCenterX(View).Minus(60),
                _textUserName.Width().EqualTo(View.Frame.Width).Minus(130),
                _textUserName.Below(_uIToolbar,60),

                _textUserPassword.WithSameCenterX(View).Minus(60),
                _textUserPassword.Width().EqualTo(View.Frame.Width).Minus(130),
                _textUserPassword.Below(_textUserName, 40),

                 _textUserPasswordRepeat.WithSameCenterX(View).Minus(60),
                _textUserPasswordRepeat.Width().EqualTo(View.Frame.Width).Minus(130),
                _textUserPasswordRepeat.Below(_textUserPassword, 40),

                _imageUserPhoto.AtRightOf(_textUserName,-100),
                _imageUserPhoto.WithSameCenterY(_textUserName),
                _imageUserPhoto.Width().EqualTo(80),
                _imageUserPhoto.Height().EqualTo(80),

                _buttonPhoto.WithSameCenterX(_imageUserPhoto),
                _buttonPhoto.Width().EqualTo(80),
                _buttonPhoto.Height().EqualTo(80),
                _buttonPhoto.Below(_imageUserPhoto,-80),

                _buttonCreate.WithSameCenterX(View),
                _buttonCreate.Width().EqualTo(View.Frame.Width).Minus(120),
                _buttonCreate.Below(_textUserPasswordRepeat, 60)
                );
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