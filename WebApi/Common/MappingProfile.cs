using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common;

// MappingProfile s�n�f�, AutoMapper taraf�ndan kullan�lan profil tan�m�n� i�erir.
public class MappingProfile : Profile
{
    // MappingProfile s�n�f�n�n constructor'�.
    public MappingProfile()
    {
        // CreateBookModel ile Book aras�nda e�leme yap�l�r.
        CreateMap<CreateBookModel, Book>();

        // Book ile BookDetailViewModel aras�nda e�leme yap�l�r.
        CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

        // Book ile BooksViewModel aras�nda e�leme yap�l�r.
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
}
