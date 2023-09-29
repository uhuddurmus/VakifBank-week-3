using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DbOperations;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Ana uygulama sunucusunu olu�turur ve �al��t�r�r

        // Ana uygulama sunucusunu olu�turur
        var host = CreateHostBuilder(args).Build();

        // Servis kapsam� (scope) olu�turur
        using (var scope = host.Services.CreateScope())
        {
            // Servis sa�lay�c�s�n� al�r
            var services = scope.ServiceProvider;

            // Veri �reteci (DataGenerator) s�n�f�n� kullanarak ba�lang�� verilerini ekler veya olu�turur
            DataGenerator.Initialize(services);
        }

        // Uygulamay� �al��t�r�r
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // Startup s�n�f�n� kullanarak ana web sunucusunu yap�land�r�r
                webBuilder.UseStartup<Startup>();
            });
}
