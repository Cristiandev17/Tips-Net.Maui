using Evertec.Tips.Domain.Entities;
using Evertec.Tips.Infrastructure.Interfaces;
using Evertec.Tips.Infrastructure.Providers;

namespace Evertec.Tips.Infrastructure.Repositories
{
    public class TipRepository : ITipRepository
    {
        private IDatabaseContextProvider _contextProvider;

        public TipRepository(IDatabaseContextProvider contextProvider)
        {
            this._contextProvider = contextProvider;
        }

        public Task<bool> DeleteTip(int id)
        {
            var result = new bool();
            var response = _contextProvider._connection.Delete<TipEntity>(id);
            if (response != 0)
                result = true;

            return Task.FromResult(result);
        }

        public Task<bool> UpdateTip(TipEntity item)
        {
            var result = new bool();
            var response = _contextProvider._connection.Update(item);
            if (response != 0)
                result = true;

            return Task.FromResult(result);
        }

        public Task<bool> AddTip(TipEntity item)
        {
            var result = new bool();
            var response = _contextProvider._connection.Insert(item);
            if (response != 0)
                result = true;

            return Task.FromResult(result);
        }

        public Task<List<TipEntity>> GetAll()
        {
            return Task.FromResult(_contextProvider._connection.Table<TipEntity>().ToList());
        }
    }
}
