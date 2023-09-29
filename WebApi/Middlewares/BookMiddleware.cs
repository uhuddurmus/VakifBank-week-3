using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares;

// BookMiddleware sýnýfý, ASP.NET Core ara yazýlýmlarýný temsil eder.
public class BookMiddleware
{
    private readonly RequestDelegate _next;

    // BookMiddleware sýnýfýnýn constructor'ý.
    public BookMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Middleware iþlemini gerçekleþtiren Invoke metodu.
    public async Task Invoke(HttpContext context)
    {
        // Ardýþýk middleware'e isteði ileten kýsým.
        await _next.Invoke(context);
    }
}

// BookMiddleware'i uygulamaya eklemek için kullanýlan uzantý sýnýfý.
static public class BookMiddlewareEntension
{
    // UseBook metodu, BookMiddleware'i uygulamaya eklemek için kullanýlýr.
    public static IApplicationBuilder UseBook(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BookMiddleware>();
    }
}
