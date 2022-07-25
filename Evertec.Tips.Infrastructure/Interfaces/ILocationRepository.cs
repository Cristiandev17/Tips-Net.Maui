using Evertec.Tips.Domain.Entities;

namespace Evertec.Tips.Infrastructure.Interfaces
{
    public interface ILocationRepository
    {
        Task<bool> DeleteLocation(int id);

        Task<bool> AddLocation(LocationEntity item);

        Task<LocationEntity> GetByTip(int id);
    }
}
