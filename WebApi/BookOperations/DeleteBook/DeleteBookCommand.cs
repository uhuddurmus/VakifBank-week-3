using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using FluentValidation;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook;

// DeleteBookCommand s�n�f�, bir kitab�n silinmesi komutunu temsil eder.
public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    // Silinecek kitab�n kimli�i (ID).
    public int BookId { get; set; }

    // DeleteBookCommand s�n�f�n�n constructor'�.
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Handle metodu, kitab�n silinme i�lemini ger�ekle�tirir.
    public void Handle()
    {


        //Validasyon i�lemleri

        var validator = new DeleteBookCommandValidator();
        var validationResult = validator.Validate(this);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Belirtilen ID'ye sahip kitab� veritaban�ndan al�r.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // E�er kitap bulunamazsa bir istisna (exception) f�rlat�l�r.
        if (book is null)
        {
            throw new InvalidOperationException("Book doesn't exist.");
        }

        // Kitab� veritaban�ndan kald�r�r.
        _dbContext.Books.Remove(book);

        // De�i�iklikleri veritaban�na kaydeder.
        _dbContext.SaveChanges();
    }
}
