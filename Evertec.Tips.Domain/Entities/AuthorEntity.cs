using SQLite;

namespace Evertec.Tips.Domain.Entities
{
    public class AuthorEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string AuthorName { get; set; }
    }
}
