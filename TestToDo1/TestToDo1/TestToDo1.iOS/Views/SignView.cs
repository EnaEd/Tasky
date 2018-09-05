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
            _BackBarButton.Title = string.Empty;
            NavigationItem.RightBarButtonItem = _BackBarButton;
            var _MenuBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "Back";
            NavigationItem.LeftBarButtonItem = _BackBarButton;

            _labelError = new UILabel();
            _labelError.TextColor = UIColor.Red;
            _labelError.Font = _labelError.Font.WithSize(10);
            Add(_labelError);

            _textUserLogin = new UITextField();
            _textUserLogin.Placeholder = "Login";
            _textUserLogin.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            Add(_textUserLogin);

            _textUserPassword = new UITextField();
            _textUserPassword.Placeholder = "Password";
            _textUserPassword.SecureTextEntry = true;
            _textUserPassword.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
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
            set.Bind(_BackBarButton).To(vm => vm.BackToCommand);
            
            set.Apply();

            //conastraint
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(_labelError.WithSameCenterX(View));
            View.AddConstraints(_labelError.AtTopOf(View, 61));
            View.AddConstraints(_labelError.Height().EqualTo(20));

            View.AddConstraints(_textUserLogin.FullWidthOf(View, 25));
            View.AddConstraints(_textUserLogin.Below(_labelError, 40));

            View.AddConstraints(_textUserPassword.FullWidthOf(View, 25));
            View.AddConstraints(_textUserPassword.Below(_textUserLogin, 60));

            View.AddConstraints(_buttonSignIn.FullWidthOf(View, 25));
            View.AddConstraints(_buttonSignIn.Below(_textUserPassword, 60));

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