using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Common;

// MappingProfile sýnýfý, AutoMapper tarafýndan kullanýlan profil tanýmýný içerir.
public class MappingProfile : Profile
{
    // MappingProfile sýnýfýnýn constructor'ý.
    public MappingProfile()
    {
        // CreateBookModel ile Book arasýnda eþleme yapýlýr.
        CreateMap<CreateBookModel, Book>();

        // Book ile BookDetailViewModel arasýnda eþleme yapýlýr.
        CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

        // Book ile BooksViewModel arasýnda eþleme yapýlýr.
        CreateMap<Book, BooksViewModel>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
}
