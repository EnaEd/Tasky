using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;

namespace TestToDo1.iOS.Views
{
    [Register("SignView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ActivePanel, true)]
    public class SignView : MvxViewController
    {
        private UIToolbar _uIToolbar;
        private UIButton _buttonSignIn;
        private UITextField _textUserLogin;
        private UITextField _textUserPassword;
        private UILabel _labelError;
        
        public SignView()
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

            _textUserLogin = new UITextField();
            _textUserLogin.Placeholder = "Login";
            Add(_textUserLogin);

            _textUserPassword = new UITextField();
            _textUserPassword.Placeholder = "Password";
            _textUserPassword.SecureTextEntry = true;
            Add(_textUserPassword);

            _buttonSignIn = new UIButton(UIButtonType.RoundedRect);
            _buttonSignIn.Layer.CornerRadius = 5;
            _buttonSignIn.BackgroundColor = UIColor.Blue;
            _buttonSignIn.SetTitleColor(UIColor.White, UIControlState.Normal);    
            _buttonSignIn.SetTitle("SignIn", UIControlState.Normal);
            Add(_buttonSignIn);

            var set = this.CreateBindingSet<SignView, SignViewModel>();
            set.Bind(_textUserLogin).To(vm => vm.UserLogin);
            set.Bind(_textUserPassword).To(vm => vm.UserPassword);
            set.Bind(_buttonSignIn).To(vm => vm.SignCommand);
            set.Bind(_labelError).To(vm => vm.Error);
            
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

                _textUserLogin.WithSameCenterX(View),
                _textUserLogin.Width().EqualTo(View.Frame.Width).Minus(120),
                _textUserLogin.Below(_uIToolbar,60),

                _textUserPassword.WithSameCenterX(View),
                _textUserPassword.Width().EqualTo(View.Frame.Width).Minus(120),
                _textUserPassword.Below(_textUserLogin, 60),

                _buttonSignIn.WithSameCenterX(View),
                _buttonSignIn.Width().EqualTo(View.Frame.Width).Minus(120),
                _buttonSignIn.Below(_textUserPassword, 60)
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
    }
}