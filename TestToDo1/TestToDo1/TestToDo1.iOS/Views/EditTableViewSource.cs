using Foundation;
using MvvmCross.Binding.iOS.Views;
using TestToDo1.Core.Repository;
using TestToDo1.iOS.Helper;
using UIKit;


namespace TestToDo1.iOS.Views
{
    //swipe delete
    public class EditTableViewSource : MvxStandardTableViewSource
    {
        public IRemove _mainViewModel;

        public EditTableViewSource(IRemove viewModel, UITableView tableView, string bindingText) : base(tableView, bindingText)
        {
            _mainViewModel = viewModel;
        }

        public EditTableViewSource(IRemove viewModel, UITableView tableView) : base(tableView)
        {
            _mainViewModel = viewModel;
            tableView.RegisterClassForCellReuse(typeof(Cell), new NSString("Cell"));
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                     _mainViewModel.RemoveCommand.Execute(indexPath.Row);
                    break;
                case UITableViewCellEditingStyle.None:
                    break;
            }
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (_mainViewModel.RemoveCommand.CanExecute(indexPath.Row))
            {
                return UITableViewCellEditingStyle.Delete;
            }
            return UITableViewCellEditingStyle.None;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return (Cell)tableView.DequeueReusableCell("Cell");
        }

    }
}