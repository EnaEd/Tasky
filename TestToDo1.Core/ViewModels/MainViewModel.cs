using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Windows.Input;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;
using MvvmCross.Plugins.PictureChooser;
using System.IO;
using TestToDo1.Core.Repository;


namespace TestToDo1.Core.ViewModels
{
    public class MainViewModel : MvxViewModel,IRemove
    {
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IItemRepository _itemRepository;
        
        public byte[] UserImage { get; set; }
        public string UserLogin { get; set; }

        #region for observableCollection using
        //private List<Item> _tempListItems;
        //public List<Item> TempListItems
        //{
        //    get
        //    {
        //        return _tempListItems;
        //    }
        //    set
        //    {
        //        _tempListItems = value;
        //        RaisePropertyChanged(() => TempListItems);
        //    }
        //}
        #endregion

        //for SQLite using
        private List<Item> _tempListItemsSQL;
        public List<Item> TempListItemsSQL
        {
            get
            {
                return _tempListItemsSQL;
            }
            set
            {
                _tempListItemsSQL = value;
                RaisePropertyChanged(() => TempListItemsSQL);
            }
        }

        public MainViewModel(IMvxPictureChooserTask pictureChooserTask, IItemRepository iitemRepository,IItemRepository itemRepository)
        {
            //for observableCollection using
            //TempListItems = new List<Item>(ListViewItem.ListItems);

            _itemRepository = itemRepository;
            _pictureChooserTask = pictureChooserTask;
            maxPixel = 400;
            qualityPercent = 90;

            //for SQLite using
            TempListItemsSQL = new List<Item>(_itemRepository.Get(SignViewModel.UserTemp.Id));

            //for photo on drawerLayout
            UserImage = SignViewModel.UserTemp.UserImage;
            UserLogin = SignViewModel.UserTemp.UserLogin;
        }

        private MvxCommand _goToItem;
        public ICommand GoToItem
        {
            get
            {
                _goToItem = _goToItem ?? new MvxCommand(GoInItem);
                return _goToItem;
            }
        }
        private void GoInItem()
        {
           ShowViewModel<ItemViewModel>();
        }

        private MvxCommand<Item> _changeItem;
        public ICommand ChangeItem
        {
            get
            {
                _changeItem = _changeItem ?? new MvxCommand<Item>(GoToChange);
                return _changeItem;

            }
        }
        private void GoToChange(Item item)
        {
           
                ShowViewModel<ItemViewModel>(item);

                #region for observableCollection using
                //TempListItems.Clear();
                //RaisePropertyChanged(() => TempListItems);
                #endregion

                //for SQLite using
                TempListItemsSQL.Clear();
                RaisePropertyChanged(() => TempListItemsSQL);
        }

        private MvxCommand _addPicture;
        public ICommand AddPictureCommand
        {
            get
            {
                _addPicture = _addPicture ?? new MvxCommand(AddPicture);
                return _addPicture;
            }
        }

        private int maxPixel;
        private int qualityPercent;

        //public for change in navigation menu
        public void AddPicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(maxPixel, qualityPercent, OnPicture, () => { });
        }

        private void OnPicture(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            //update user photo
            SignViewModel.UserTemp.UserImage = memoryStream.ToArray();
            Mvx.Resolve<IUserRepository>().Save(SignViewModel.UserTemp);
            ShowViewModel<MainViewModel>();
        }

        //for navigationMenu
        public void ShowSelf()
        {
            ShowViewModel<MainViewModel>();
        }
        public void ShowLogView()
        {
            ShowViewModel<LogInViewModel>();
        }

        //for iOS leftPanel
        public void Show()
        {
            ShowViewModel<LeftPanelViewModel>();
        }

        private MvxCommand _backToCommand;
        public ICommand BackToCommand
        {
            get
            {
                _backToCommand = _backToCommand ?? new MvxCommand(GoBack);
                return _backToCommand;
            }
        }
        private void GoBack()
        {
            ShowViewModel<SignViewModel>();
        }

        //for swipe delete iOS
        private MvxCommand<int> _removeCommand;
        public ICommand RemoveCommand
        {
            get => _removeCommand ?? (_removeCommand = new MvxCommand<int>(i =>
            {
                //try in iPhone....
                _itemRepository.Delete(TempListItemsSQL[i].Id);
                ShowViewModel<MainViewModel>();
            }));
        }
    }
}