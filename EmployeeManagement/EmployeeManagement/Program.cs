using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("EmployeeDb"); });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();
        app.UseCors("CorsPolicy");

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}