using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations;

// BookStoreDbContext s�n�f�, veritaban� ba�lant�s�n� y�neten bir DbContext s�n�f�n� temsil eder.
public class BookStoreDbContext : DbContext
{
    // BookStoreDbContext s�n�f�n�n constructor'�.
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {

    }

    // Books �zelli�i, veritaban�ndaki Kitaplar tablosunu temsil eder.
    public DbSet<Book> Books { get; set; }
}
