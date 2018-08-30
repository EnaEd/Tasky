using MvvmCross.Core.ViewModels;
using System;
using System.Windows.Input;
using TestToDo1.Core.Models;
using TestToDo1.Core.IRepository;
using MvvmCross.Plugins.PhoneCall;
using MvvmCross.Plugins.PictureChooser;
using System.IO;

namespace TestToDo1.Core.ViewModels
{
    public class ItemViewModel : MvxViewModel
    {
        public Item Item { get;set; }

        private readonly IMvxPhoneCallTask _phoneCallTask;
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

        private int _maxPixel;
        private int _qualityPercent;

        public byte[] UserImage { get; set; }
        public string UserLogin { get; set; }

        public string TaskName
        {
            get
            {
                var name = String.Empty;
                if (TaskDone == true)
                {
                    name = (TaskContent.Length < 10)
                        ?  $"✓ {Item.TaskContent}"
                        :  $"✓ {TaskContent.Substring(0, 10)}...";

                    return name;
                }
                    name = (TaskContent.Length < 10)
                        ? name = $"{Item.TaskContent}"
                        :name = $"{TaskContent.Substring(0, 10)}...";
                return name;
            }
        }
        public string TaskContent
        {
            get { return Item.TaskContent; }
            set
            {
                if (Item.TaskContent!=value)
                {
                    Item.TaskContent = value;
                    RaisePropertyChanged(() => TaskContent);
                    RaisePropertyChanged(() => TaskName); 
                }
            }
        }
        public bool TaskDone
        {
            get { return Item.TaskDone; }
            set
            {
                if (Item.TaskDone != value)
                {
                    Item.TaskDone = value;
                    RaisePropertyChanged(() => TaskDone);
                    RaisePropertyChanged(() => TaskName);
                }
            }
        }

        public string ContactName
        {
            get
            {
                return (String.IsNullOrEmpty(Item.ContactName))
                       ?""
                       : Item.ContactName;
            }
            set
            {
                Item.ContactName = value;
                RaisePropertyChanged(() => ContactName);
            }
        }
        public string ContactPhone
        {
            get
            {
                return Item.ContactPhone;
            }
            set
            {
                Item.ContactPhone = value;
                RaisePropertyChanged(() => ContactPhone);
            }
        }

        public ItemViewModel(IMvxPhoneCallTask phoneCallTask, IMvxPictureChooserTask pictureChooserTask, IItemRepository itemRepository,IUserRepository userRepository)
        {
            //for photo on drawerLayout
            UserImage = SignViewModel.UserTemp.UserImage;
            UserLogin = SignViewModel.UserTemp.UserLogin;

            _phoneCallTask = phoneCallTask;
            _pictureChooserTask = pictureChooserTask;
            _itemRepository = itemRepository;
            _userRepository = userRepository;

            _maxPixel = 400;
            _qualityPercent = 90;
        }

        public void Init(Item item)
        {
            Item = item;
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
        //public for catch hardware button back event
        public void GoBack()
        {
            ShowViewModel<MainViewModel>();
        }

        private MvxCommand _saveItem;
        public ICommand SaveItem
        {
            get
            {
                _saveItem = _saveItem ?? new MvxCommand(DoSaveItem);
                return _saveItem;
            }
        }

        private void DoSaveItem()
        {
            if (!string.IsNullOrEmpty(TaskContent))
            {
                #region useSQLite
                Item.TaskName = this.TaskName;
                Item.TaskContent = this.TaskContent;
                Item.TaskDone = this.TaskDone;
                Item.ContactName = this.ContactName;
                Item.ContactPhone = this.ContactPhone;
                Item.UserId = SignViewModel.UserTemp.Id;
                _itemRepository.Save(Item);
                #endregion

                #region use observsableCollection
                //for (int i = 0; i < ListViewItem.ListItems.Count; i++)
                //{
                //    if (ListViewItem.ListItems[i].TaskContent.Equals(this.TaskContent))
                //    {
                //        ListViewItem.ListItems.RemoveAt(i);
                //    }
                //}

                //Item.TaskName = this.TaskName;
                //Item.TaskContent = this.TaskContent;
                //Item.TaskDone = this.TaskDone;
                //ListViewItem.ListItems.Add(Item);
                #endregion
            }
            GoBack();
        }

        private MvxCommand _deleteItem;
        public ICommand DeleteItem       {
            get
            {
                _deleteItem = _deleteItem ?? new MvxCommand(DoDeleteItem);
                return _deleteItem;
            }
        }
        private void DoDeleteItem()
        {
            #region useSQLite
            _itemRepository.Delete(Item.Id);
            #endregion

            #region use observsableCollection

            //for (int i = 0; i < ListViewItem.ListItems.Count; i++)
            //{
            //    if (ListViewItem.ListItems[i].TaskContent.Equals(this.TaskContent))
            //    {
            //        ListViewItem.ListItems.RemoveAt(i);
            //    }
            //}
            #endregion
            GoBack();
        }

        private MvxCommand _switchClick;
        public ICommand SwitchClick
        {
            get
            {
                _switchClick = _switchClick ?? new MvxCommand(DoSwitchClick);
                return _switchClick;
            }
        }
        private void DoSwitchClick()
        {
            TaskDone = !TaskDone;
        }

        private MvxCommand _phoneCallCommand;
        public ICommand PhoneCallCommand
        {
            get
            {
                _phoneCallCommand = _phoneCallCommand ?? new MvxCommand(DoPhoneCall);
                return _phoneCallCommand;
            }
        }
        private void DoPhoneCall()
        {

            if (String.IsNullOrEmpty(ContactPhone))
            {
                ContactPhone = "";
            }
            
            _phoneCallTask.MakePhoneCall("",ContactPhone); 
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
        //public for change in navigation menu
        public void AddPicture()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(_maxPixel, _qualityPercent, OnPicture, () => { });
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

        private void OnPicture(Stream stream)
        {
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            //update user photo
            SignViewModel.UserTemp.UserImage = memoryStream.ToArray();
            _userRepository.Save(SignViewModel.UserTemp);
            ShowViewModel<ItemViewModel>();
        }

        //for iOS leftPanel
        public void Show()
        {
            ShowViewModel<LeftPanelViewModel>();
        }
        public void ShowContact(Item Item)
        {
            ShowViewModel<ContactViewModel>(Item);
        }
    }   
}