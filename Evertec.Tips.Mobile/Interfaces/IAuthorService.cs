using Evertec.Tips.Domain.Models;

namespace Evertec.Tips.Mobile.Interfaces
{
    public interface IAuthorService
    {
        Task<bool> AddAuthor(AuthorModel item);

        Task<List<AuthorModel>> GetAll();
    }
}
