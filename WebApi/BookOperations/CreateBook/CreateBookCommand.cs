using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook;

// CreateBookCommand sýnýfý, yeni bir kitap oluþturma komutunu temsil eder.
public class CreateBookCommand
{
    // Yeni kitap oluþturmak için kullanýlacak veri modeli.
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    // CreateBookCommand sýnýfýnýn constructor'ý.
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Handle metodu, yeni kitap oluþturma iþlemini gerçekleþtirir.
    public void Handle()
    {
        // Kitap baþlýðýna göre veritabanýnda mevcut bir kitap arar.
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        // Eðer kitap zaten mevcutsa bir istisna (exception) fýrlatýlýr.
        if (book is not null)
        {
            throw new InvalidOperationException("Book already exists.");
        }

        // Yeni kitap modelini Book sýnýfýna dönüþtürür.
        book = _mapper.Map<Book>(Model);

        // Yeni kitabý veritabanýna ekler.
        _dbContext.Books.Add(book);

        // Deðiþiklikleri veritabanýna kaydeder.
        _dbContext.SaveChanges();
    }

    // CreateBookModel sýnýfý, yeni kitap oluþturmak için kullanýlan veri modelini temsil eder.
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
