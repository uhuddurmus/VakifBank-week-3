using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DbOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // Entity Framework için veritabaný baðlantýsýný saðlayan bir DbContext oluþturuyoruz.
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Eðer veritabanýnda kitaplar zaten mevcutsa, iþlemi sonlandýrýyoruz.
            if (context.Books.Any())
            {
                return;
            }

            // Yeni kitaplar eklemek için bir liste oluþturuyoruz.
            context.Books.AddRange(
                new Book
                {
                    //Id=1,
                    Title = "Lean Startup",
                    GenreId = 1, // Kiþisel Geliþim
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book
                {
                    //Id=2,
                    Title = "Herland",
                    GenreId = 2, // Bilim Kurgu
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    //Id=3,
                    Title = "Dune",
                    GenreId = 2, // Bilim Kurgu
                    PageCount = 540,
                    PublishDate = new DateTime(2002, 12, 21)
                },
                new Book
                {
                    // Id=4,
                    Title = "The Murder Room: In which Three of the Greatest Detectives Use Forensic Science to Solve the World's Most Perplexing Cold Cases",
                    GenreId = 3, // Gerçek Suç
                    PageCount = 464,
                    PublishDate = new DateTime(2009, 01, 01)
                }
            );

            // Yaptýðýmýz deðiþiklikleri veritabanýna kaydediyoruz.
            context.SaveChanges();
        }
    }
}
