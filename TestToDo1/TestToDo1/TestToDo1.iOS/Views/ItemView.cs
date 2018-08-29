using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using AddressBookUI;

namespace TestToDo1.iOS.Views
{
    [Register("ItemView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ItemView : MvxViewController<ItemViewModel>
    {
        private UIButton _buttonSave;
        private UIButton _buttonDelete;
        private UIButton _buttonCall;
        private UILabel _labelTaskName;
        private UITextField _textEdit;
        private UITextField _textContactName;
        private UITextField _textContactPhone;
        private UISwitch _swith;
        private UILabel _labelPeopleContact;
        private UIButton _buttonSelectContact;
        private ABPeoplePickerNavigationController _peopleConroller;

        public ItemView()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            ViewModel.Show();

            var _BackBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "Back";
            NavigationItem.RightBarButtonItem = _BackBarButton;

            _peopleConroller = new ABPeoplePickerNavigationController();
            _peopleConroller.SelectPerson2 += SelectPeople;
            _peopleConroller.Cancelled += delegate {
                this.DismissModalViewController(true);
            };

            _labelTaskName = new UILabel();
            Add(_labelTaskName);

            _textEdit = new UITextField();
            _textEdit.Placeholder = "todo...";
            Add(_textEdit);

            _textContactName = new UITextField();
            _textContactName.Placeholder = "add contact...";
            Add(_textContactName);

            _textContactPhone = new UITextField();
            _textContactPhone.Placeholder = "add Phone...";
            Add(_textContactPhone);

            _labelPeopleContact = new UILabel();
            Add(_labelPeopleContact);

            _buttonSelectContact = new UIButton();
            _buttonSelectContact.BackgroundColor = UIColor.Blue;
            _buttonSelectContact.SetTitle("Add", UIControlState.Normal);
            _buttonSelectContact.Layer.CornerRadius = 5;
            _buttonSelectContact.Layer.BorderWidth = 1;
            _buttonSelectContact.TouchUpInside += delegate { PresentModalViewController(_peopleConroller, true); };
            Add(_buttonSelectContact);

            _buttonCall = new UIButton();
            _buttonCall.BackgroundColor = UIColor.Blue;
            _buttonCall.SetTitle("Call", UIControlState.Normal);
            _buttonCall.Layer.CornerRadius = 5;
            _buttonCall.Layer.BorderWidth = 1;
            Add(_buttonCall);

            _swith = new UISwitch();
            _swith.On = true;
            Add(_swith);

            _buttonSave = new UIButton(UIButtonType.Custom);
            _buttonSave.BackgroundColor = UIColor.Blue;
            _buttonSave.Layer.CornerRadius = 5;
            _buttonSave.Layer.BorderWidth = 1;
            _buttonSave.SetTitle("Save", UIControlState.Normal);
            Add(_buttonSave);

            _buttonDelete = new UIButton(UIButtonType.Custom);
            _buttonDelete.Layer.CornerRadius = 5;
            _buttonDelete.Layer.BorderWidth = 1;
            _buttonDelete.BackgroundColor = UIColor.Blue;
            _buttonDelete.SetTitle("Delete", UIControlState.Normal);
            _buttonDelete.TitleColor(UIControlState.Selected);
            Add(_buttonDelete);

            var set = this.CreateBindingSet<ItemView, ItemViewModel>();
            set.Bind(_BackBarButton).To(vm => vm.BackToCommand);
            set.Bind(_labelTaskName).To(vm => vm.TaskName);
            set.Bind(_textEdit).To(vm => vm.TaskContent);
            set.Bind(_swith).To(vm => vm.TaskDone);
            set.Bind(_buttonSave).To(vm => vm.SaveItem);
            set.Bind(_buttonDelete).To(vm => vm.DeleteItem);
            set.Bind(_textContactName).To(vm => vm.ContactName);
            set.Bind(_textContactPhone).To(vm => vm.ContactPhone);
            set.Bind(_buttonCall).To(vm => vm.PhoneCallCommand);
            set.Apply();

            //conastraint
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(

                 _labelTaskName.WithSameCenterX(View),
                 _labelTaskName.WithSameTop(View).Plus(100),
                 _labelTaskName.Width().EqualTo(View.Frame.Width).Minus(130),
                 _labelTaskName.Height().EqualTo(80),

                 _textEdit.WithSameCenterX(_labelTaskName),
                 _textEdit.Below(_labelTaskName,20),
                 _textEdit.Width().EqualTo(View.Frame.Width).Minus(130),
                 _textEdit.Height().EqualTo(60),

                 _textContactName.WithSameCenterX(_textEdit),
                 _textContactName.Below(_textEdit,20),
                 _textContactName.Width().EqualTo(View.Frame.Width).Minus(130),
                 _textContactName.Height().EqualTo(40),

                 _textContactPhone.WithSameCenterX(_textContactName),
                 _textContactPhone.Below(_textContactName, 20),
                 _textContactPhone.Width().EqualTo(View.Frame.Width).Minus(130),
                 _textContactPhone.Height().EqualTo(40),

                 _buttonSelectContact.WithSameRight(View).Minus(20),
                 _buttonSelectContact.Below(_textEdit, 20),
                 _buttonSelectContact.Width().EqualTo(80),

                 _swith.WithSameLeft(View).Plus(20),
                 _swith.Below(_textContactPhone, 20),

                 _buttonCall.Below(_buttonSelectContact, 20),
                 _buttonCall.WithSameRight(View).Minus(20),
                 _buttonCall.Width().EqualTo(80),

                 _buttonSave.WithSameLeft(View).Plus(20),
                 _buttonSave.Below(_swith,40),
                 _buttonSave.Width().EqualTo(80),

                 _buttonDelete.WithSameRight(View).Minus(20),
                 _buttonDelete.Below(_swith, 40),
                 _buttonDelete.Width().EqualTo(80)

                );
        }

        private void SelectPeople(object sender, ABPeoplePickerSelectPerson2EventArgs e)
        {
            ViewModel.ContactName = $"{e.Person.LastName} {e.Person.FirstName}";
            ViewModel.ContactPhone = (e.Person.GetPhones()).ToString();
            this.DismissModalViewController(true);
        }
    }
}