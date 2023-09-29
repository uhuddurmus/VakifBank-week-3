using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook;

// UpdateBookCommand s�n�f�, bir kitab�n g�ncellenmesi komutunu temsil eder.
public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    // Kitap kimli�i (ID) ve g�ncelleme verileri i�eren model.
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }

    // UpdateBookCommand s�n�f�n�n constructor'�.
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Handle metodu, kitap g�ncelleme i�lemini ger�ekle�tirir.
    public void Handle()
    {
        // Belirtilen ID'ye sahip kitab� veritaban�ndan al�r.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // E�er kitap bulunamazsa bir istisna (exception) f�rlat�l�r.
        if (book is null)
        {
            throw new InvalidOperationException("Book doesn't exist.");
        }

        // E�er g�ncelleme verileri belirtilmi�se, kitab�n �zelliklerini g�nceller.
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        // De�i�iklikleri veritaban�na kaydeder.
        _dbContext.SaveChanges();
    }
}

// UpdateBookModel s�n�f�, kitap g�ncelleme i�lemi i�in kullan�lan veri modelini temsil eder.
public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
}
