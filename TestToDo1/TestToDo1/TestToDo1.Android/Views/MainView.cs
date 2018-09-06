using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget.Helper;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using TestToDo1.Core.ViewModels;
using TestToDo1.Droid.Helper;

namespace TestToDo1.Droid.Views
{
    [Activity(Label = "ListItems" , Theme = "@style/MyTheme")]
    public class MainView : MvxActivity<MainViewModel>
    {
        private SwipeRefreshLayout _swipeContainer;
        private NavigationView _navigationView;
        private DrawerLayout _drawerLayout;
        private MvxRecyclerView _recyclerView;
        private ItemTouchHelper _itemTouchHelper;
        private string _filePath;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            //save user
            _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"ToDoUser.txt");
            File.WriteAllText(_filePath,$"{SignViewModel.UserCurrent.UserLogin}`" +
                                        $"{SignViewModel.UserCurrent.UserPassword}");

            _recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.taskList);
            _itemTouchHelper = new ItemTouchHelper(new Swipe2DismissTouchHelperCallback(this));
            _itemTouchHelper.AttachToRecyclerView(_recyclerView);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id._drawerMain);

            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            _navigationView.NavigationItemSelected += NavigationViewClick;

            //navigationHeader
            var headerView = _navigationView.GetHeaderView(0);
            headerView.Click += AddPhoto;

            _swipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeContainer1);
            _swipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
            _swipeContainer.Refresh += SwipeContainer_Refresh;
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
            _drawerLayout.CloseDrawers();
        }

        async void SwipeContainer_Refresh(object sender, EventArgs e)
        {
            //for test...
            await Task.Delay(3000);
            ViewModel.ShowSelf();
            (sender as SwipeRefreshLayout).Refreshing = false;
        }
    }
}
