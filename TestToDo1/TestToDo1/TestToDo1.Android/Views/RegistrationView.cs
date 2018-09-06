using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using TestToDo1.Core.ViewModels;

namespace TestToDo1.Droid.Views
{
    [Activity(Label = "Registration", Theme = "@style/MyTheme")]
    public class RegistrationView : MvxActivity<RegistrationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RegistrationView);
        }
    }
}
