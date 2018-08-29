using System.Collections.ObjectModel;

namespace TestToDo1.Core.Models
{
    public static class ListViewItem
    {
        public static ObservableCollection<Item> ListItems { get; set; }
        static ListViewItem()
        {
            ListItems = new ObservableCollection<Item>();

            ListItems.Add(new Item { TaskName = "todo1", TaskContent = "todo1", TaskDone = false });
            ListItems.Add(new Item { TaskName = "todo2", TaskContent = "todo2", TaskDone = true});
            ListItems.Add(new Item { TaskName = "todo3", TaskContent = "todo3",TaskDone = false });
        }

    }
}
