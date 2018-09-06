using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using TestToDo1.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestToDo1.Core.ViewModels
{
    public class ContactViewModel : MvxViewModel
    {
        public List<Contact> Contacts { get; set; }
        private List<Contact> _filteredList;
        public List<Contact> FilteredList
        {
            get { return _filteredList; }
        }

        public Item Item { get; set; }
        private string _searchPattern;
        public string SearchPattern
        {
            get { return _searchPattern; }
            set
            {
                _searchPattern = value;
                if (string.IsNullOrEmpty(value))
                {
                    _filteredList = Contacts;
                }
                else
                { 
                    _filteredList = Contacts.Where(o => o.ContactName.ToLower().Contains(value)).ToList();
                }
                RaisePropertyChanged(() => SearchPattern);
                RaisePropertyChanged(() => FilteredList);
            }
        }


        public ContactViewModel()
        {
            Contacts = new List<Contact>();
            _filteredList = new List<Contact>();
            Contacts = FilteredList;
            
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