
using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P05Shop.API.Services;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Services.WeatherSeervice;

namespace P05Shop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IMeteoService, MeteoService>();

            // addScoped - oznacza, że w trakcie jednego requestu będzie istniała tylko jedna instancja klasy ProductService
            // addTransient - oznacza, że obiekt będzie tworzony za każdym razem, gdy odwolujemy się do niego
            // addSingleton - oznacza, że obiekt będzie tworzony tylko raz i będzie istniał tak długo, jak długo istnieje aplikacja



            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
