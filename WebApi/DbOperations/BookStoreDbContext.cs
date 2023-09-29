using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations;

// BookStoreDbContext sýnýfý, veritabaný baðlantýsýný yöneten bir DbContext sýnýfýný temsil eder.
public class BookStoreDbContext : DbContext
{
    // BookStoreDbContext sýnýfýnýn constructor'ý.
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {

    }

    // Books özelliði, veritabanýndaki Kitaplar tablosunu temsil eder.
    public DbSet<Book> Books { get; set; }
}
