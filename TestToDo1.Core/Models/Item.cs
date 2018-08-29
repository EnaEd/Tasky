using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TestToDo1.Core.Models
{
    [Table("Items")]
    public class Item: AbstractModel
    {
        public string TaskName { get; set; }

        public string TaskContent{ get; set; }

        public bool TaskDone { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}
