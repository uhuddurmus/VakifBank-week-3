using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    // Bu sýnýf, veritabanýndan kitaplarý çekmek için kullanýlan sorgu sýnýfýdýr.
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; // Veritabaný baðlantýsý için DbContext
            _mapper = mapper;       // DTO ve model dönüþümü için AutoMapper
        }

        // Kitaplarý veritabanýndan almak için kullanýlan metot.
        public List<BooksViewModel> Handle()
        {
            // Veritabanýndan kitaplarý Id'ye göre sýralayarak alýyoruz ve bir liste oluþturuyoruz.
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

            // Oluþturduðumuz kitap listesini, BooksViewModel tipine dönüþtürmek için AutoMapper kullanarak bir view model listesine çeviriyoruz.
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            // Son olarak, view model listesini döndürüyoruz.
            return vm;
        }
    }

    // Bu sýnýf, kitaplarýn görüntülenmesi için kullanýlan view model sýnýfýdýr.
    public class BooksViewModel
    {
        public string Title { get; set; }         // Kitap baþlýðý
        public int PageCount { get; set; }        // Sayfa sayýsý
        public string PublishDate { get; set; }    // Yayýn tarihi
        public string Genre { get; set; }         // Kitap türü
    }
}
