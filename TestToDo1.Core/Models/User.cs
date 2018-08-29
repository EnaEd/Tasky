using SQLite;

namespace TestToDo1.Core.Models
{
    [Table("Users")]
    public class User:AbstractModel
    {
        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public byte[] UserImage { get; set; }
    }
}
