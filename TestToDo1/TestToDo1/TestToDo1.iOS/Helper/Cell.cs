using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using TestToDo1.Core.ViewModels;
using UIKit;

namespace TestToDo1.iOS.Helper
{
    [Register("Cell")]
    class Cell: MvxTableViewCell
    {

        private UILabel _taskName;
        private UILabel _contactPhone;
        

        public Cell()
        {
            CreateLayout();
            InitializeBindings();
        }

        public Cell(IntPtr handle)
            : base(handle)
        {
            CreateLayout();
            InitializeBindings();
        }

        private void CreateLayout()
        {
            const int offsetStart = 10;
            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            _taskName = new UILabel(new RectangleF(offsetStart, 0, 75, 40));
            _contactPhone = new UILabel(new RectangleF(76, 0, 200, 40));
            _contactPhone.TextAlignment = UITextAlignment.Right;
            _contactPhone.Font = _contactPhone.Font.WithSize(15);
            ContentView.AddSubviews(_taskName,_contactPhone);
        }


        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<Cell, ItemViewModel>();
                set.Bind(_taskName).To(vm => vm.TaskName);
                set.Bind(_contactPhone).To(vm => vm.ContactPhone);
                set.Apply();
            });
        }



    }
}