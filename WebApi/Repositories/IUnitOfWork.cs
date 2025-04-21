using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        Task<int> CommitAsync();
    }

}
