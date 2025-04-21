using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if (genre == null)
                throw new InvalidOperationException("Güncellenecek tür bulunamadı.");

            if (!string.IsNullOrWhiteSpace(Model.Name))
                genre.Name = Model.Name;

            _context.SaveChanges();
        }
    }
}
