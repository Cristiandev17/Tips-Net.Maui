using Evertec.Tips.Domain.Entities;
using SQLite;

namespace Evertec.Tips.Infrastructure.Providers
{
    public class DatabaseContextProvider : IDatabaseContextProvider
    {
        public SQLiteConnection _connection { get; }

        public DatabaseContextProvider()
        {
            _connection = new (Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Tips.db3"));
            _connection.CreateTable<TipEntity>();
            _connection.CreateTable<AuthorEntity>();
            _connection.CreateTable<LocationEntity>();
        }

    }
}
