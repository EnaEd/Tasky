using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using TestToDo1.Core.Models;
using System.Collections.Generic;

namespace TestToDo1.Core.ViewModels
{
    public class ContactViewModel : MvxViewModel
    {
        public List<Contact> Contacts { get; set; }
        public Item Item { get; set; }

        public ContactViewModel()
        {
            Contacts = new List<Contact>();
        }
        public void Init(Item item)
        {
            Item = item;
        }

        private MvxCommand<Contact> _changeItem;
        public ICommand ChangeItem
        {
            get
            {
                _changeItem = _changeItem ?? new MvxCommand<Contact>(GoToChange);
                return _changeItem;

            }
        }
        private void GoToChange(Contact contact)
        {
            Item.ContactName = contact.ContactName;
            Item.ContactPhone = contact.ContactPhone;
            ShowViewModel<ItemViewModel>(Item);
        }

    }   
}