using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using System.Threading.Tasks;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using System.IO;

namespace TestToDo1.iOS.Views
{
    [Register("MainView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ActivePanel, true)]
    public class MainView : MvxViewController<MainViewModel>
    {
        private MvxUIRefreshControl _refresh;
        private UITableView _table;
        private bool _isLoad;
        private string _filePath;

        public MainView()
        {
            
        }
        public override void ViewDidLoad()
        {
            View = new UIView() { BackgroundColor= UIColor.White};

            base.ViewDidLoad();

            ViewModel.Show();

            //save user
            _filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ToDoUser.txt");
            File.WriteAllText(_filePath, $"{SignViewModel.UserCurrent.UserLogin}`" +
                                        $"{SignViewModel.UserCurrent.UserPassword}");

            var _addBarButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            _addBarButton.Title = string.Empty;
            NavigationItem.RightBarButtonItem = _addBarButton;

            _table = new UITableView();
            _table.RowHeight = 60;
            RefreshAsync();
            AddRefreshControl();
            _table.AddSubview(_refresh);
            var _source = new EditTableViewSource(ViewModel, _table, "TitleText TaskName + ContactPhone");
            _table.Source = _source;
            Add(_table);

           var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(_addBarButton).To(vm => vm.GoToItem);
            set.Bind(_source).To(vm => vm.TempListItemsSQL);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.ChangeItem);
            #region with mvvmcross Command
            //set.Bind(_refresh).For(v => v.IsRefreshing).To(vm => vm.IsLoad);
            //set.Bind(_refresh).For(r => r.Message).To(vm => vm.LoadMessage);
            //set.Bind(_refresh).For(v => v.RefreshCommand).To(vm => vm.RefreshCommand);
            #endregion

            set.Apply();

            _table.ReloadData();

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.AddConstraints(_table.FullWidthOf(View));
            View.AddConstraints(_table.FullHeightOf(View));
        }

        private void RefreshAsync()
        {
            if (_isLoad)
            {
                _refresh.BeginRefreshing();
            }
            if (_isLoad)
            {
                _refresh.EndRefreshing();
            }
        }

        private void AddRefreshControl()
        {
            _refresh = new MvxUIRefreshControl();
            _refresh.ValueChanged += async (sender, e) =>
            {
                //for test
                await Task.Delay(1000);
                RefreshAsync();
            };
            _isLoad = true;
        }

        
    }
}