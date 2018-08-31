using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;

namespace TestToDo1.iOS.Views
{
    [Register("LogInView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class LogInView : MvxViewController
    {
        private UIToolbar _uIToolbar;
        private UIButton _buttonSignIn;
        private UIButton _buttonNewUser;

        public LogInView()
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

            _buttonSignIn = new UIButton(UIButtonType.System);
            _buttonSignIn.SetTitle("SIGNIN", UIControlState.Normal);
            _buttonSignIn.Layer.CornerRadius = 5;
            _buttonSignIn.BackgroundColor = UIColor.Blue;
            _buttonSignIn.SetTitleColor(UIColor.White, UIControlState.Normal);
            Add(_buttonSignIn);

            _buttonNewUser = new UIButton(UIButtonType.System);
            _buttonNewUser.SetTitle("REGISTRATION", UIControlState.Normal);
            _buttonNewUser.Layer.CornerRadius = 5;
            _buttonNewUser.BackgroundColor = UIColor.Blue;
            _buttonNewUser.SetTitleColor(UIColor.White, UIControlState.Normal);
            Add(_buttonNewUser);

            var set = this.CreateBindingSet<LogInView, LogInViewModel>();
            set.Bind(_buttonSignIn).To(vm => vm.SignCommand);
            set.Bind(_buttonNewUser).To(vm => vm.RegistrationCommand);

            set.Apply();


            //conastraint
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _uIToolbar.WithSameCenterX(View),
                _uIToolbar.WithSameTop(View).Plus(60),
                _uIToolbar.Width().EqualTo(View.Frame.Width).Minus(0),

                _buttonSignIn.WithSameCenterX(View),
                _buttonSignIn.WithSameCenterY(View).Minus(100),
                _buttonNewUser.WithSameCenterX(View),

                _buttonSignIn.Width().EqualTo(View.Frame.Width).Minus(100),
                _buttonNewUser.Width().EqualTo(View.Frame.Width).Minus(100),

                _buttonNewUser.Below(_buttonSignIn, 40)
                );
            //for disable swipe
            CreateGestureRecognizer();
        }
        void CreateGestureRecognizer()//call this method in the ViewDidLoad override class.
        {
            UIPanGestureRecognizer swipeRecognizer = new UIPanGestureRecognizer();
            swipeRecognizer.AddTarget(() => HandleSwipe(swipeRecognizer));
            View.AddGestureRecognizer(swipeRecognizer);
        }

        void HandleSwipe(UIPanGestureRecognizer sender)
        {
            //You don't need implement any code here.
        }
    }
}