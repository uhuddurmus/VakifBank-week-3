using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares;

// BookMiddleware s�n�f�, ASP.NET Core ara yaz�l�mlar�n� temsil eder.
public class BookMiddleware
{
    private readonly RequestDelegate _next;

    // BookMiddleware s�n�f�n�n constructor'�.
    public BookMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Middleware i�lemini ger�ekle�tiren Invoke metodu.
    public async Task Invoke(HttpContext context)
    {
        // Ard���k middleware'e iste�i ileten k�s�m.
        await _next.Invoke(context);
    }
}

// BookMiddleware'i uygulamaya eklemek i�in kullan�lan uzant� s�n�f�.
static public class BookMiddlewareEntension
{
    // UseBook metodu, BookMiddleware'i uygulamaya eklemek i�in kullan�l�r.
    public static IApplicationBuilder UseBook(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BookMiddleware>();
    }
}
