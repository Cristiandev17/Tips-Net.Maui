using Evertec.Tips.Domain.Mappers;
using Evertec.Tips.Domain.Models;
using Evertec.Tips.Infrastructure.Interfaces;
using Evertec.Tips.Mobile.Interfaces;

namespace Evertec.Tips.Mobile.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> AddAuthor(AuthorModel item)
        {
            var authorEntity = await AuthorMapper.MapAuthorEntity(item);
            return await _authorRepository.AddAuthor(authorEntity);
        }

        public async Task<List<AuthorModel>> GetAll()
        {
            var list = await _authorRepository.GetAll();
            return await AuthorMapper.MapAuthorsModel(list.OrderBy(l => l.Id).ToList());
        }
    }
}
