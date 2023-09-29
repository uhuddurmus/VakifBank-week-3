using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail;

// Bu s�n�f, belirli bir kitab�n ayr�nt�lar�n� getirmek i�in kullan�lan sorgu s�n�f�d�r.
public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    // Sorgu i�in kullan�lacak kitab�n kimli�ini temsil eden �zellik.
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext; // Veritaban� ba�lant�s� i�in DbContext
        _mapper = mapper;       // DTO ve model d�n���m� i�in AutoMapper
    }

    // Belirli bir kitab�n ayr�nt�lar�n� getirmek i�in kullan�lan metot.
    public BookDetailViewModel Handle()
    {
        // Veritaban�ndan, belirtilen kimli�e sahip kitab� buluyoruz veya varsay�lan olarak null d�n�yoruz.
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

        // E�er kitap bulunamazsa, bir istisna f�rlat�yoruz.
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamad�.");
        }

        // Kitab�, BookDetailViewModel tipine d�n��t�rmek i�in AutoMapper kullanarak bir view model olu�turuyoruz.
        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

        // Olu�turdu�umuz view modeli d�nd�r�yoruz.
        return vm;
    }
}

// Bu s�n�f, kitab�n ayr�nt�lar�n� g�r�nt�lemek i�in kullan�lan view model s�n�f�d�r.
public class BookDetailViewModel
{
    public string Title { get; set; }         // Kitap ba�l���
    public int PageCount { get; set; }        // Sayfa say�s�
    public string PublishDate { get; set; }    // Yay�n tarihi
    public string Genre { get; set; }         // Kitap t�r�
}
