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
using CoreAnimation;

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
        private UILabel _labelError;
        private UITextField _textEdit;
        private UITextField _textContactName;
        private UITextField _textContactPhone;
        private UISwitch _swith;
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
            View.BackgroundColor = UIColor.FromRGB(204, 242, 255);

            var border = new CALayer();
            nfloat width = 2;
            border.BorderColor = UIColor.Black.CGColor;
            border.BorderWidth = width;

            ViewModel.Show();

            _contentConteiner = new UIView();
            _scrollView = new UIScrollView();
            _scrollView.AddSubview(_contentConteiner);
            Add(_scrollView);

            var _BackBarButton = new UIBarButtonItem();
            _BackBarButton.Title = "Back";
            NavigationItem.LeftBarButtonItem = _BackBarButton;

            _peopleConroller = new ABPeoplePickerNavigationController();
            _peopleConroller.SelectPerson2 += SelectPeople;
            _peopleConroller.Cancelled += delegate
            {
                this.DismissModalViewController(true);
            };

            _labelTaskName = new UILabel();
            _contentConteiner.AddSubview(_labelTaskName);


            _labelError = new UILabel();
            _labelError.Font = _labelError.Font.WithSize(10);
            _labelError.TextColor = UIColor.Red;
            _contentConteiner.AddSubview(_labelError);

            _textEdit = new UITextField();
            _textEdit.Placeholder = "todo...";
            _textEdit.BorderStyle = UITextBorderStyle.RoundedRect;
            _textEdit.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textEdit);

            _textContactName = new UITextField();
            _textContactName.Placeholder = "add contact...";
            _textContactName.BorderStyle = UITextBorderStyle.RoundedRect;
            _textContactName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            _contentConteiner.AddSubview(_textContactName);

            _textContactPhone = new UITextField();
            _textContactPhone.Placeholder = "add Phone...";
            _textContactPhone.KeyboardType = UIKeyboardType.NamePhonePad;
            _textContactPhone.ReturnKeyType = UIReturnKeyType.Done;
            _textContactPhone.BorderStyle = UITextBorderStyle.RoundedRect;
            _textContactPhone.ShouldReturn = (textField) =>
            {
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
            set.Bind(_labelError).To(vm => vm.Error);
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

                 _labelError.AtTopOf(_contentConteiner),
                 _labelError.WithSameWidth(_contentConteiner),
                 _labelError.WithSameCenterX(_contentConteiner),
                 _labelError.Height().EqualTo(20),

                 _labelTaskName.WithSameCenterX(_contentConteiner),
                 _labelTaskName.Below(_labelError, 25),
                 _labelTaskName.WithSameWidth(_contentConteiner).Minus(130),
                 _labelTaskName.Height().LessThanOrEqualTo(60),

                 _textEdit.WithSameCenterX(_contentConteiner),
                 _textEdit.Below(_labelTaskName, 20),
                 _textEdit.WithSameWidth(_contentConteiner).Minus(25),
                 _textEdit.Height().LessThanOrEqualTo(60),

                 _textContactName.AtLeftOf(_contentConteiner, 25),
                 _textContactName.Below(_textEdit, 20),
                 _textContactName.WithSameWidth(_contentConteiner).Minus(130),
                 _textContactName.Height().EqualTo(40),

                 _textContactPhone.AtLeftOf(_contentConteiner, 25),
                 _textContactPhone.Below(_textContactName, 20),
                 _textContactPhone.WithSameWidth(_contentConteiner).Minus(130),
                 _textContactPhone.Height().EqualTo(40),

                 _buttonSelectContact.ToRightOf(_textContactName, 20),
                 _buttonSelectContact.WithSameCenterY(_textContactName),
                 _buttonSelectContact.Width().EqualTo(80),

                 _swith.AtLeftOf(_contentConteiner, 25),
                 _swith.Below(_textContactPhone, 20),

                 _buttonCall.WithSameCenterY(_textContactPhone),
                 _buttonCall.ToRightOf(_textContactPhone, 20),
                 _buttonCall.Width().EqualTo(80),

                 _buttonSave.AtLeftOf(_contentConteiner, 25),
                 _buttonSave.Below(_swith, 40),
                 _buttonSave.Width().EqualTo(80),
                 _buttonSave.Height().LessThanOrEqualTo(35),

                 _buttonDelete.WithSameCenterX(_buttonCall),
                 _buttonDelete.Below(_swith, 40),
                 _buttonDelete.Width().EqualTo(80),
                 _buttonDelete.Height().LessThanOrEqualTo(35)

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
            
            bool result = listPhone.Any();
            if (result)
            {
                string phoneResult = Convert.ToString(listPhone.FirstOrDefault().Value);
                phoneResult = phoneResult.Replace(" ", "");

                ViewModel.ContactPhone = phoneResult;
            }
            this.DismissModalViewController(true);
        }
    }
}