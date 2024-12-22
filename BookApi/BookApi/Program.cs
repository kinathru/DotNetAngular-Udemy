namespace BookApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // add services
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyCors", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        var app = builder.Build();

        // add mapping
        app.MapControllers();
        app.UseCors("MyCors");
        app.MapGet("/", () => Results.Redirect("/api/books"));

        app.Run();
    }
}