using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any() || context.Authors.Any())
                {
                    return;
                }

                // Önce yazarları ekleyelim
                var author1 = new Author
                {
                    FirstName = "James",
                    LastName = "Clear",
                    BirthDate = new DateTime(1986, 1, 1)
                };

                var author2 = new Author
                {
                    FirstName = "Alfred",
                    LastName = "Bester",
                    BirthDate = new DateTime(1913, 12, 18)
                };

                var author3 = new Author
                {
                    FirstName = "Dan",
                    LastName = "Simmons",
                    BirthDate = new DateTime(1948, 4, 4)
                };

                var author4 = new Author
                {
                    FirstName = "John",
                    LastName = "Douglas",
                    BirthDate = new DateTime(1945, 6, 18)
                };

                context.Authors.AddRange(author1, author2, author3, author4);
                context.SaveChanges();

                // Kitapları yazarlarla bağlayarak ekleyelim
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Atomic Habits",
                        GenreId = 1,
                        PageCount = 320,
                        PublishDate = new DateTime(2018, 10, 16),
                        AuthorId = author1.Id
                    },
                    new Book
                    {
                        Title = "The Stars My Destination",
                        GenreId = 2,
                        PageCount = 288,
                        PublishDate = new DateTime(1956, 12, 12),
                        AuthorId = author2.Id
                    },
                    new Book
                    {
                        Title = "Hyperion",
                        GenreId = 2,
                        PageCount = 482,
                        PublishDate = new DateTime(1989, 05, 01),
                        AuthorId = author3.Id
                    },
                    new Book
                    {
                        Title = "Mindhunter: Inside the FBI's Elite Serial Crime Unit",
                        GenreId = 3,
                        PageCount = 368,
                        PublishDate = new DateTime(1995, 01, 03),
                        AuthorId = author4.Id
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
