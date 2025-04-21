using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Services.Abstract
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto> GetByIdAsync(int id);
        Task AddAsync(CreateAuthorDto dto);
        Task UpdateAsync(UpdateAuthorDto dto);
        Task DeleteAsync(int id);
    }
}
