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

// Bu s�n�f, Web API'deki kitap i�lemlerini y�netmek i�in kullan�l�r.
[ApiController]
[Route("api/[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        // Controller s�n�f�n�n constructor'�, gerekli ba��ml�l�klar� al�r.
        _context = context; // Veritaban� ba�lant�s� i�in DbContext
        _mapper = mapper;   // DTO ve model d�n���m� i�in AutoMapper
    }

    // T�m kitaplar� almak i�in HTTP GET iste�ine yan�t veren metod.
    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    // Belirli bir kitab� almak i�in HTTP GET iste�ine yan�t veren metod.
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        BookDetailViewModel result;
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = id;
        result = query.Handle();
        return Ok(result);
    }

    // Yeni bir kitap eklemek i�in HTTP POST iste�ine yan�t veren metod.
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = newBook;
        command.Handle();
        return Ok();
    }

    // Bir kitab� g�ncellemek i�in HTTP PUT iste�ine yan�t veren metod.
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = id;
        command.Model = updatedBook;
        command.Handle();
        return Ok();
    }

    // Bir kitab� silmek i�in HTTP DELETE iste�ine yan�t veren metod.
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = id;
        command.Handle();
        return Ok();
    }
}
