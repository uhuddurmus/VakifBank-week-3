using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers;

// Bu sýnýf, Web API'deki kitap iþlemlerini yönetmek için kullanýlýr.
[ApiController]
[Route("api/[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        // Controller sýnýfýnýn constructor'ý, gerekli baðýmlýlýklarý alýr.
        _context = context; // Veritabaný baðlantýsý için DbContext
        _mapper = mapper;   // DTO ve model dönüþümü için AutoMapper
    }

    // Tüm kitaplarý almak için HTTP GET isteðine yanýt veren metod.
    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    // Belirli bir kitabý almak için HTTP GET isteðine yanýt veren metod.
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        BookDetailViewModel result;
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = id;
        result = query.Handle();
        return Ok(result);
    }

    // Yeni bir kitap eklemek için HTTP POST isteðine yanýt veren metod.
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = newBook;
        command.Handle();
        return Ok();
    }

    // Bir kitabý güncellemek için HTTP PUT isteðine yanýt veren metod.
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;
        command.Handle();
        return Ok();
    }

    // Bir kitabý silmek için HTTP DELETE isteðine yanýt veren metod.
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = id;
        command.Handle();
        return Ok();
    }
}
