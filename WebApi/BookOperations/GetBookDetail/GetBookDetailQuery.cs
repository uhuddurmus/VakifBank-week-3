using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail;

// Bu sýnýf, belirli bir kitabýn ayrýntýlarýný getirmek için kullanýlan sorgu sýnýfýdýr.
public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    // Sorgu için kullanýlacak kitabýn kimliðini temsil eden özellik.
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext; // Veritabaný baðlantýsý için DbContext
        _mapper = mapper;       // DTO ve model dönüþümü için AutoMapper
    }

    // Belirli bir kitabýn ayrýntýlarýný getirmek için kullanýlan metot.
    public BookDetailViewModel Handle()
    {
        // Veritabanýndan, belirtilen kimliðe sahip kitabý buluyoruz veya varsayýlan olarak null dönüyoruz.
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

        // Eðer kitap bulunamazsa, bir istisna fýrlatýyoruz.
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamadý.");
        }

        // Kitabý, BookDetailViewModel tipine dönüþtürmek için AutoMapper kullanarak bir view model oluþturuyoruz.
        BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);

        // Oluþturduðumuz view modeli döndürüyoruz.
        return vm;
    }
}

// Bu sýnýf, kitabýn ayrýntýlarýný görüntülemek için kullanýlan view model sýnýfýdýr.
public class BookDetailViewModel
{
    public string Title { get; set; }         // Kitap baþlýðý
    public int PageCount { get; set; }        // Sayfa sayýsý
    public string PublishDate { get; set; }    // Yayýn tarihi
    public string Genre { get; set; }         // Kitap türü
}
