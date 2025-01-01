using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Data;
using WebAPI.Domain.Entities;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<Entities>(
            options =>
            {
                var connectionString =
                    "Server=localhost,61077;" +
                    "Database=Flights;" +
                    "User Id=flights_api;" +
                    "Password=1234!Secret;" +
                    "TrustServerCertificate=True;";
                options.UseSqlServer(connectionString);
            });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddServer(new OpenApiServer
            {
                Description = "Development Server",
                Url = "https://localhost:7155"
            });
            options.CustomOperationIds(e =>
                $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
        });

        builder.Services.AddScoped<Entities>();

        var app = builder.Build();

        var entities = app.Services.CreateScope().ServiceProvider.GetRequiredService<Entities>();
        entities.Database.EnsureCreated();

        List<Flight> flightsToSeed =
        [
            new(
                Guid.NewGuid(),
                "Airways Alpha",
                "$450",
                new TimePlace("Los Angeles", new DateTime(2024, 12, 28, 10, 0, 0)),
                new TimePlace("New York", new DateTime(2024, 12, 28, 14, 30, 0)),
                2
            ),

            new(
                Guid.NewGuid(),
                "Bravo Airlines",
                "$320",
                new TimePlace("Houston", new DateTime(2024, 12, 28, 15, 0, 0)),
                new TimePlace("Chicago", new DateTime(2024, 12, 28, 18, 0, 0)),
                15
            ),

            new(
                Guid.NewGuid(),
                "SkyHigh",
                "$280",
                new TimePlace("Seattle", new DateTime(2024, 12, 28, 11, 0, 0)),
                new TimePlace("San Francisco", new DateTime(2024, 12, 28, 13, 45, 0)),
                25
            ),

            new(
                Guid.NewGuid(),
                "Delta Express",
                "$400",
                new TimePlace("Atlanta", new DateTime(2024, 12, 28, 17, 0, 0)),
                new TimePlace("Miami", new DateTime(2024, 12, 28, 19, 30, 0)),
                10
            ),

            new(
                Guid.NewGuid(),
                "Echo Flights",
                "$380",
                new TimePlace("Denver", new DateTime(2024, 12, 28, 16, 30, 0)),
                new TimePlace("Boston", new DateTime(2024, 12, 28, 20, 15, 0)),
                5
            ),

            new(
                Guid.NewGuid(),
                "Foxtrot Wings",
                "$150",
                new TimePlace("Phoenix", new DateTime(2024, 12, 28, 10, 0, 0)),
                new TimePlace("Las Vegas", new DateTime(2024, 12, 28, 12, 30, 0)),
                30
            ),

            new(
                Guid.NewGuid(),
                "Global Air",
                "$310",
                new TimePlace("Dallas", new DateTime(2024, 12, 28, 18, 0, 0)),
                new TimePlace("Orlando", new DateTime(2024, 12, 28, 21, 0, 0)),
                8
            ),

            new(
                Guid.NewGuid(),
                "Horizon",
                "$220",
                new TimePlace("Portland", new DateTime(2024, 12, 28, 13, 0, 0)),
                new TimePlace("Salt Lake City", new DateTime(2024, 12, 28, 15, 45, 0)),
                12
            ),

            new(
                Guid.NewGuid(),
                "Inland Airways",
                "$330",
                new TimePlace("Charlotte", new DateTime(2024, 12, 28, 20, 0, 0)),
                new TimePlace("Washington D.C.", new DateTime(2024, 12, 28, 22, 30, 0)),
                7
            ),

            new(
                Guid.NewGuid(),
                "Jet Stream",
                "$270",
                new TimePlace("Detroit", new DateTime(2024, 12, 28, 21, 15, 0)),
                new TimePlace("Minneapolis", new DateTime(2024, 12, 28, 23, 45, 0)),
                18
            )
        ];
        entities.Flights.AddRange(flightsToSeed);
        entities.SaveChanges();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                options.RoutePrefix = string.Empty; // display swagger UI for root path
            });
        }

        app.UseCors("CorsPolicy");
        app.MapControllers();

        app.Run();
    }
}