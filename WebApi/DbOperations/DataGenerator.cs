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
                if (context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange( 
                    new Book
                    {
                        //Id=1,
                        Title = "Atomic Habits",
                        GenreId = 1, // Personal Growth
                        PageCount = 320,
                        PublishDate = new DateTime(2018, 10, 16)
                    },
                    new Book
                    {
                        //Id=2,
                        Title = "The Stars My Destination",
                        GenreId = 2, // Science Fiction
                        PageCount = 288,
                        PublishDate = new DateTime(1956, 12, 12)
                    },
                    new Book
                    {
                        //Id=3,
                        Title = "Hyperion",
                        GenreId = 2, // Science Fiction
                        PageCount = 482,
                        PublishDate = new DateTime(1989, 05, 01)
                    },
                    new Book
                    {
                        //Id=4,
                        Title = "Mindhunter: Inside the FBI's Elite Serial Crime Unit",
                        GenreId = 3, // True Crime
                        PageCount = 368,
                        PublishDate = new DateTime(1995, 01, 03)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
