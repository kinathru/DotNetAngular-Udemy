using BookApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

// https://localhost:xyz/api/books
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly Book[] _books =
    [
        new() { Id = 1, Author = "Author One", Title = "Book One" },
        new() { Id = 2, Author = "Author Two", Title = "Book Two" },
        new() { Id = 3, Author = "Author Three", Title = "Book Three" }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return Ok(_books);
    }
}