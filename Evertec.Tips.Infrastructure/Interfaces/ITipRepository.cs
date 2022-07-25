using Evertec.Tips.Domain.Entities;

namespace Evertec.Tips.Infrastructure.Interfaces
{
    public interface ITipRepository
    {
        Task<bool> DeleteTip(int id);

        Task<bool> UpdateTip(TipEntity item);

        Task<bool> AddTip(TipEntity item);

        Task<List<TipEntity>> GetAll();
    }
}
