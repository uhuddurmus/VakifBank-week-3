using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{
    // Bu s�n�f, veritaban�ndan kitaplar� �ekmek i�in kullan�lan sorgu s�n�f�d�r.
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; // Veritaban� ba�lant�s� i�in DbContext
            _mapper = mapper;       // DTO ve model d�n���m� i�in AutoMapper
        }

        // Kitaplar� veritaban�ndan almak i�in kullan�lan metot.
        public List<BooksViewModel> Handle()
        {
            // Veritaban�ndan kitaplar� Id'ye g�re s�ralayarak al�yoruz ve bir liste olu�turuyoruz.
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

            // Olu�turdu�umuz kitap listesini, BooksViewModel tipine d�n��t�rmek i�in AutoMapper kullanarak bir view model listesine �eviriyoruz.
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);

            // Son olarak, view model listesini d�nd�r�yoruz.
            return vm;
        }
    }

    // Bu s�n�f, kitaplar�n g�r�nt�lenmesi i�in kullan�lan view model s�n�f�d�r.
    public class BooksViewModel
    {
        public string Title { get; set; }         // Kitap ba�l���
        public int PageCount { get; set; }        // Sayfa say�s�
        public string PublishDate { get; set; }    // Yay�n tarihi
        public string Genre { get; set; }         // Kitap t�r�
    }
}
