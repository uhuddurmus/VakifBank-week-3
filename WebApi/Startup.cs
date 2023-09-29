using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Hizmetlerin (services) yap�land�r�lmas�
    public void ConfigureServices(IServiceCollection services)
    {
        // JSON d�n���m� i�in Newtonsoft.Json kullan�lmas�n� ekler
        services.AddControllers()
        .AddNewtonsoftJson();

        // Swagger belgelemesini yap�land�r�r
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
        });

        // Entity Framework ile bellekte bir veritaban� kullan�lmas�n� yap�land�r�r
        services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));

        // ILoggerService arabirimini uygulayan bir ConsoleLogger s�n�f�n� ekler
        services.AddSingleton<ILoggerService, ConsoleLogger>();

        // AutoMapper kullan�m�n� yap�land�r�r
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    // Uygulaman�n yap�land�r�lmas�
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Hata sayfas�n� geli�tirme ortam�nda kullan�r
            app.UseDeveloperExceptionPage();
            // Swagger belgelendirmesini etkinle�tirir
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
        }

        // HTTPS y�nlendirmesini etkinle�tirir
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        // �zel istisna (exception) orta��n� kullan�r
        app.UseCustomExceptionMiddle();

        app.UseEndpoints(endpoints =>
        {
            // Kontrolleri e�le�tirir
            endpoints.MapControllers();
        });
    }
}
