using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Runtime;
using MvvmCross.Droid.Views;

namespace TestToDo1.Droid.Views
{
    [Activity(Label = "LogInView",Theme ="@style/MyTheme")]
    public class LogInView : MvxActivity
    {        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LogInView);
        }
        
    }
}
