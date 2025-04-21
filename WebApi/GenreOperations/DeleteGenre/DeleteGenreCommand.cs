using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if (genre == null)
                throw new InvalidOperationException("Silinecek tür bulunamadı.");

            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}
