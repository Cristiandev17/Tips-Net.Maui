using Evertec.Tips.Domain.Entities;
using Evertec.Tips.Infrastructure.Providers;
using Evertec.Tips.Infrastructure.Interfaces;

namespace Evertec.Tips.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private IDatabaseContextProvider _contextProvider;

        public LocationRepository(IDatabaseContextProvider contextProvider)
        {
            this._contextProvider = contextProvider;
        }

        public Task<bool> DeleteLocation(int id)
        {
            var result = new bool();
            var response = _contextProvider._connection.Delete<LocationEntity>(id);
            if (response != 0)
                result = true;

            return Task.FromResult(result);
        }

        public Task<bool> AddLocation(LocationEntity item)
        {
            var result = new bool();
            var response = _contextProvider._connection.Insert(item);
            if (response != 0)
                result = true;

            return Task.FromResult(result);
        }

        public Task<LocationEntity> GetByTip(int id)
        {
            return Task.FromResult(_contextProvider._connection.Table<LocationEntity>().FirstOrDefault(l => l.TipId == id));
        }
    }
}
