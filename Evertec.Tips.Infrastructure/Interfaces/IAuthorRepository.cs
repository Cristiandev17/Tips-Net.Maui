using Evertec.Tips.Domain.Entities;

namespace Evertec.Tips.Infrastructure.Interfaces
{
    public interface IAuthorRepository
    {
        Task<bool> AddAuthor(AuthorEntity item);

        Task<List<AuthorEntity>> GetAll();
    }
}
