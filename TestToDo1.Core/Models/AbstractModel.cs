using SQLite;

namespace TestToDo1.Core.Models
{
    public class AbstractModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
