using SQLite;

namespace Evertec.Tips.Infrastructure.Providers
{
    public interface IDatabaseContextProvider
    {
        SQLiteConnection _connection { get; }
    }
}
