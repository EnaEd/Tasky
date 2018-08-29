using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using TestToDo1.Core.ViewModels;



namespace TestToDo1.Droid.Views
{
    [Activity(Label = "Sign",Theme = "@style/MyTheme")]
    public class SignView : MvxActivity<SignViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SignView);
        }
    }
}
