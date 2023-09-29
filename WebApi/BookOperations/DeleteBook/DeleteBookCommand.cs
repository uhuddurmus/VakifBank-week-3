using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using FluentValidation;
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook;

// DeleteBookCommand sýnýfý, bir kitabýn silinmesi komutunu temsil eder.
public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    // Silinecek kitabýn kimliði (ID).
    public int BookId { get; set; }

    // DeleteBookCommand sýnýfýnýn constructor'ý.
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Handle metodu, kitabýn silinme iþlemini gerçekleþtirir.
    public void Handle()
    {


        //Validasyon iþlemleri

        var validator = new DeleteBookCommandValidator();
        var validationResult = validator.Validate(this);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Belirtilen ID'ye sahip kitabý veritabanýndan alýr.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // Eðer kitap bulunamazsa bir istisna (exception) fýrlatýlýr.
        if (book is null)
        {
            throw new InvalidOperationException("Book doesn't exist.");
        }

        // Kitabý veritabanýndan kaldýrýr.
        _dbContext.Books.Remove(book);

        // Deðiþiklikleri veritabanýna kaydeder.
        _dbContext.SaveChanges();
    }
}
