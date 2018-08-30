using UIKit;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using TestToDo1.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using Cirrious.FluentLayouts.Touch;
using AddressBookUI;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private UIScrollView _scrollView;
        private UIView _contentConteiner;

        public ItemView()
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            ViewModel.Show();

            _contentConteiner = new UIView();
            _scrollView = new UIScrollView();
            _scrollView.AddSubview(_contentConteiner);
            Add(_scrollView);

            var _BackBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "Back";
            NavigationItem.RightBarButtonItem = _BackBarButton;

            _peopleConroller = new ABPeoplePickerNavigationController();
            _peopleConroller.SelectPerson2 += SelectPeople;
            _peopleConroller.Cancelled += delegate {
                this.DismissModalViewController(true);
            };

            _labelTaskName = new UILabel();
            _contentConteiner.AddSubview(_labelTaskName);

            _textEdit = new UITextField();
            _textEdit.Placeholder = "todo...";
            _textEdit.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textEdit);

            _textContactName = new UITextField();
            _textContactName.Placeholder = "add contact...";
            _textContactName.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textContactName);

            _textContactPhone = new UITextField();
            _textContactPhone.Placeholder = "add Phone...";
            _textContactPhone.KeyboardType = UIKeyboardType.NamePhonePad;
            _textContactPhone.ReturnKeyType = UIReturnKeyType.Done;
            _textContactPhone.ShouldReturn = (textField) => {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textContactPhone);

            _buttonSelectContact = new UIButton();
            _buttonSelectContact.BackgroundColor = UIColor.Blue;
            _buttonSelectContact.SetTitle("Add", UIControlState.Normal);
            _buttonSelectContact.Layer.CornerRadius = 5;
            _buttonSelectContact.Layer.BorderWidth = 1;
            _buttonSelectContact.TouchUpInside += delegate { PresentModalViewController(_peopleConroller, true); };
            _contentConteiner.AddSubview(_buttonSelectContact);

            _buttonCall = new UIButton();
            _buttonCall.BackgroundColor = UIColor.Blue;
            _buttonCall.SetTitle("Call", UIControlState.Normal);
            _buttonCall.Layer.CornerRadius = 5;
            _buttonCall.Layer.BorderWidth = 1;
            _contentConteiner.AddSubview(_buttonCall);

            _swith = new UISwitch();
            _swith.On = true;
            _contentConteiner.AddSubview(_swith);

            _buttonSave = new UIButton(UIButtonType.Custom);
            _buttonSave.BackgroundColor = UIColor.Blue;
            _buttonSave.Layer.CornerRadius = 5;
            _buttonSave.Layer.BorderWidth = 1;
            _buttonSave.SetTitle("Save", UIControlState.Normal);
            _contentConteiner.AddSubview(_buttonSave);

            _buttonDelete = new UIButton(UIButtonType.Custom);
            _buttonDelete.Layer.CornerRadius = 5;
            _buttonDelete.Layer.BorderWidth = 1;
            _buttonDelete.BackgroundColor = UIColor.Blue;
            _buttonDelete.SetTitle("Delete", UIControlState.Normal);
            _buttonDelete.TitleColor(UIControlState.Selected);
            _contentConteiner.AddSubview(_buttonDelete);

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
            _scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            _scrollView.AddConstraints(_contentConteiner.FullWidthOf(_scrollView));
            _scrollView.AddConstraints(_contentConteiner.FullHeightOf(_scrollView));

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(_scrollView.FullWidthOf(View));
            View.AddConstraints(_scrollView.FullHeightOf(View));
            View.AddConstraints(
                _contentConteiner.WithSameWidth(View),
                _contentConteiner.WithSameHeight(View).SetPriority(UILayoutPriority.DefaultLow)
            );

            _contentConteiner.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            _contentConteiner.AddConstraints(

                 _labelTaskName.AtLeftOf(_contentConteiner,25),
                 _labelTaskName.AtTopOf(_contentConteiner,25),
                 _labelTaskName.WithSameWidth(_contentConteiner).Minus(130),
                 _labelTaskName.Height().EqualTo(80),

                 _textEdit.AtLeftOf(_contentConteiner, 25),
                 _textEdit.Below(_labelTaskName,20),
                 _textEdit.WithSameWidth(_contentConteiner).Minus(130),
                 _textEdit.Height().EqualTo(60),

                 _textContactName.AtLeftOf(_contentConteiner, 25),
                 _textContactName.Below(_textEdit,20),
                 _textContactName.WithSameWidth(_contentConteiner).Minus(130),
                 _textContactName.Height().EqualTo(40),

                 _textContactPhone.AtLeftOf(_contentConteiner, 25),
                 _textContactPhone.Below(_textContactName, 20),
                 _textContactPhone.WithSameWidth(_contentConteiner).Minus(130),
                 _textContactPhone.Height().EqualTo(40),

                 _buttonSelectContact.AtRightOf(_contentConteiner,25),
                 _buttonSelectContact.Below(_textEdit, 20),
                 _buttonSelectContact.Width().EqualTo(80),

                 _swith.AtLeftOf(_contentConteiner,25),
                 _swith.Below(_textContactPhone, 20),

                 _buttonCall.Below(_buttonSelectContact, 20),
                 _buttonCall.AtRightOf(_contentConteiner,25),
                 _buttonCall.Width().EqualTo(80),

                 _buttonSave.AtLeftOf(_contentConteiner,25),
                 _buttonSave.Below(_swith,40),
                 _buttonSave.Width().EqualTo(80),

                 _buttonDelete.AtRightOf(_contentConteiner,25),
                 _buttonDelete.Below(_swith, 40),
                 _buttonDelete.Width().EqualTo(80)

                );
            // very important to make scrolling work
            var bottomViewConstraint = _contentConteiner.Subviews.Last()
               .AtBottomOf(_contentConteiner).Minus(20);
            _contentConteiner.AddConstraints(bottomViewConstraint);
        }

        private void SelectPeople(object sender, ABPeoplePickerSelectPerson2EventArgs e)
        {
            ViewModel.ContactName = $"{e.Person.LastName} {e.Person.FirstName}";
            var listPhone = e.Person.GetPhones();

            if (listPhone.Count>0)
            {
                char[] phoneCharArray = listPhone[0].Value.ToCharArray();
                string phoneresult = String.Empty;
                for(int i=0;i<phoneCharArray.Length;i++)
                {
                    if (!phoneCharArray[i].Equals(' '))
                    {
                        phoneresult +=phoneCharArray[i] ;
                    }
                }
                ViewModel.ContactPhone = phoneresult;
            }
                this.DismissModalViewController(true);
        }
    }
}