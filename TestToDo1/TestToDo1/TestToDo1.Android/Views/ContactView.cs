using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Widget;
using MvvmCross.Droid.Views;
using TestToDo1.Core.Models;
using TestToDo1.Core.ViewModels;


namespace TestToDo1.Droid.Views
{
    [Activity(Label = "Contact", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ContactView : MvxActivity<ContactViewModel>
    {
        private ListView _contactList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ContactView);

            _contactList = FindViewById<ListView>(Resource.Id.listContacts);

            var uriPhone = ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projectionPhone = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayNamePrimary,
                ContactsContract.CommonDataKinds.Phone.Number,
            };
            var loaderPhone = new CursorLoader(this, uriPhone, projectionPhone, null, null, null);
            var cursorPhone = (ICursor)loaderPhone.LoadInBackground();
            if (cursorPhone.MoveToFirst())
            {
                for(;cursorPhone.MoveToNext();)
                {
                    int contactId = cursorPhone.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.Id);
                    string contactName = cursorPhone.GetString(cursorPhone.GetColumnIndex(
                                                              ContactsContract.Contacts.InterfaceConsts.DisplayNamePrimary));
                    string contactPhone = cursorPhone.GetString(cursorPhone.GetColumnIndex(
                                                              ContactsContract.CommonDataKinds.Phone.Number));

                    Contact contact = new Contact();
                    contact.Id = contactId;
                    contact.ContactName = contactName;
                    contact.ContactPhone = contactPhone;

                    ViewModel.Contacts.Add(contact);
                } 
            }
        }
    }
}