using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.GenreOperations.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }

        public GetGenreDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public Genre Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if (genre == null)
                throw new InvalidOperationException("Tür bulunamadı.");
            return genre;
        }
    }
}
