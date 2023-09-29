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

    // Hizmetlerin (services) yapýlandýrýlmasý
    public void ConfigureServices(IServiceCollection services)
    {
        // JSON dönüþümü için Newtonsoft.Json kullanýlmasýný ekler
        services.AddControllers()
        .AddNewtonsoftJson();

        // Swagger belgelemesini yapýlandýrýr
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
        });

        // Entity Framework ile bellekte bir veritabaný kullanýlmasýný yapýlandýrýr
        services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));

        // ILoggerService arabirimini uygulayan bir ConsoleLogger sýnýfýný ekler
        services.AddSingleton<ILoggerService, ConsoleLogger>();

        // AutoMapper kullanýmýný yapýlandýrýr
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    // Uygulamanýn yapýlandýrýlmasý
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Hata sayfasýný geliþtirme ortamýnda kullanýr
            app.UseDeveloperExceptionPage();
            // Swagger belgelendirmesini etkinleþtirir
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
        }

        // HTTPS yönlendirmesini etkinleþtirir
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        // Özel istisna (exception) ortaðýný kullanýr
        app.UseCustomExceptionMiddle();

        app.UseEndpoints(endpoints =>
        {
            // Kontrolleri eþleþtirir
            endpoints.MapControllers();
        });
    }
}
