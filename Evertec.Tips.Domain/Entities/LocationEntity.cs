using SQLite;

namespace Evertec.Tips.Domain.Entities
{
    public class LocationEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int TipId { get; set; }
    }
}
