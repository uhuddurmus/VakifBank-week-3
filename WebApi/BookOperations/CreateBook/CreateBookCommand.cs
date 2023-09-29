using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook;

// CreateBookCommand s�n�f�, yeni bir kitap olu�turma komutunu temsil eder.
public class CreateBookCommand
{
    // Yeni kitap olu�turmak i�in kullan�lacak veri modeli.
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    // CreateBookCommand s�n�f�n�n constructor'�.
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Handle metodu, yeni kitap olu�turma i�lemini ger�ekle�tirir.
    public void Handle()
    {
        // Kitap ba�l���na g�re veritaban�nda mevcut bir kitap arar.
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        // E�er kitap zaten mevcutsa bir istisna (exception) f�rlat�l�r.
        if (book is not null)
        {
            throw new InvalidOperationException("Book already exists.");
        }

        // Yeni kitap modelini Book s�n�f�na d�n��t�r�r.
        book = _mapper.Map<Book>(Model);

        // Yeni kitab� veritaban�na ekler.
        _dbContext.Books.Add(book);

        // De�i�iklikleri veritaban�na kaydeder.
        _dbContext.SaveChanges();
    }

    // CreateBookModel s�n�f�, yeni kitap olu�turmak i�in kullan�lan veri modelini temsil eder.
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
