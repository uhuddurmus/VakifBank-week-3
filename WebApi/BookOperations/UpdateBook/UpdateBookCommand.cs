using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook;

// UpdateBookCommand sýnýfý, bir kitabýn güncellenmesi komutunu temsil eder.
public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    // Kitap kimliði (ID) ve güncelleme verileri içeren model.
    public int BookId { get; set; }
    public UpdateBookModel Model { get; set; }

    // UpdateBookCommand sýnýfýnýn constructor'ý.
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Handle metodu, kitap güncelleme iþlemini gerçekleþtirir.
    public void Handle()
    {
        // Belirtilen ID'ye sahip kitabý veritabanýndan alýr.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // Eðer kitap bulunamazsa bir istisna (exception) fýrlatýlýr.
        if (book is null)
        {
            throw new InvalidOperationException("Book doesn't exist.");
        }

        // Eðer güncelleme verileri belirtilmiþse, kitabýn özelliklerini günceller.
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;

        // Deðiþiklikleri veritabanýna kaydeder.
        _dbContext.SaveChanges();
    }
}

// UpdateBookModel sýnýfý, kitap güncelleme iþlemi için kullanýlan veri modelini temsil eder.
public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
}
