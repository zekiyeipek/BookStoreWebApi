using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext _context;
        public IAuthorRepository Authors { get; }

        public UnitOfWork(BookStoreDbContext context, IAuthorRepository authorRepository)
        {
            _context = context;
            Authors = authorRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
