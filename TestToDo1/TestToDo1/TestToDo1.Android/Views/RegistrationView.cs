using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using MvvmCross.Droid.Views;
using System;
using System.IO;
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
