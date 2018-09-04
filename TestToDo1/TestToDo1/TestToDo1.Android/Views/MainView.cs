using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget.Helper;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using Refractored.Fab;
using System;
using System.Threading.Tasks;
using TestToDo1.Core.IRepository;
using TestToDo1.Core.ViewModels;
using TestToDo1.Droid.Helper;

namespace TestToDo1.Droid.Views
{
    [Activity(Label = "ListItems" , Theme = "@style/MyTheme")]
    public class MainView : MvxActivity<MainViewModel>
    {
        private SwipeRefreshLayout swipeContainer;
        private NavigationView navigationView;
        private DrawerLayout drawerLayout;
        private MvxRecyclerView recyclerView;
        private ItemTouchHelper itemTouchHelper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.taskList);
            itemTouchHelper = new ItemTouchHelper(new Swipe2DismissTouchHelperCallback(this));
            itemTouchHelper.AttachToRecyclerView(recyclerView);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id._drawerMain);

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationViewClick;

            //navigationHeader
            var headerView = navigationView.GetHeaderView(0);
            headerView.Click += AddPhoto;

            swipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeContainer1);
            swipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
            swipeContainer.Refresh += SwipeContainer_Refresh;
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
                SignViewModel.UserTemp.IsLogged = false;
                Mvx.Resolve<IUserRepository>().Save(SignViewModel.UserTemp);
                ViewModel.ShowLogView();
            }
            drawerLayout.CloseDrawers();
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
