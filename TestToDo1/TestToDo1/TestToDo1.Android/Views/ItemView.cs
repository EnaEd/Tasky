﻿using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;
using MvvmCross.Droid.Views;
using TestToDo1.Core.ViewModels;

namespace TestToDo1.Droid.Views
{
    [Activity(Label = "Item", Theme = "@style/MyTheme")]
    public class ItemView : MvxActivity<ItemViewModel>
    {
        private Switch _switch;
        private Button _buttonAdd;
        private NavigationView navigationView;
        private DrawerLayout drawerLayout;
        private string _path;
        private string _filePath;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ItemView);

            //save user
            _path = Application.Context.FilesDir.Path;
            _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "User.txt");
            File.WriteAllText(_filePath, $"{SignViewModel.UserTemp.UserLogin}." +
                                        $"{SignViewModel.UserTemp.UserPassword}");

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id._drawerItemView);

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationViewClick;

            //navigationHeader
            var headerView = navigationView.GetHeaderView(0);
            headerView.Click += AddPhoto;

            _buttonAdd = FindViewById<Button>(Resource.Id.buttonAdd);
            _buttonAdd.Click += AddContact;

            _switch = FindViewById<Switch>(Resource.Id.switchDone);
            _switch.TextOff = "Not Done";
            _switch.TextOn = "Done";
            _switch.CheckedChange += SwitchClick;

        }

        private void AddPhoto(object sender, EventArgs e)
        {
            ViewModel.AddPicture();
        }

        private void NavigationViewClick(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);
            if (e.MenuItem.ItemId == Resource.Id.nav_home)
            {
                ViewModel.ShowSelf();
            }
            if (e.MenuItem.ItemId == Resource.Id.nav_logOff)
            {
                File.Delete(_filePath);
                ViewModel.ShowLogView();
            }
            drawerLayout.CloseDrawers();
        }

        //hardware event back
        public override void OnBackPressed()
        {
            ViewModel.GoBack();
        }

        private void AddContact(object sender, EventArgs e)
        {
            ViewModel.ShowContact(ViewModel.Item);
        }

        private void SwitchClick(object sender, EventArgs e)
        {
            _switch.Text = (_switch.Checked) ? "Done" : "Not Done";
        }
    }
}