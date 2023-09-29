using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DbOperations;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        // Ana uygulama sunucusunu oluşturur ve çalıştırır

        // Ana uygulama sunucusunu oluşturur
        var host = CreateHostBuilder(args).Build();

        // Servis kapsamı (scope) oluşturur
        using (var scope = host.Services.CreateScope())
        {
            // Servis sağlayıcısını alır
            var services = scope.ServiceProvider;

            // Veri üreteci (DataGenerator) sınıfını kullanarak başlangıç verilerini ekler veya oluşturur
            DataGenerator.Initialize(services);
        }

        // Uygulamayı çalıştırır
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // Startup sınıfını kullanarak ana web sunucusunu yapılandırır
                webBuilder.UseStartup<Startup>();
            });
}
