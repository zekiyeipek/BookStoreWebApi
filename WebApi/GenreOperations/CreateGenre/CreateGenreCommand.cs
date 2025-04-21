using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.GenreOperations.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genreExists = _context.Genres.Any(g => g.Name == Model.Name);
            if (genreExists)
                throw new InvalidOperationException("Tür zaten mevcut.");

            var genre = new Genre { Name = Model.Name };
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
}
