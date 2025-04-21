using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author author);
        void Remove(Author author);
    }
}
