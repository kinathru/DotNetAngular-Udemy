using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.ReadModels;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController(Entities entities) : ControllerBase
{
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<BookingRm>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<BookingRm>> List(string email)
    {
        var bookings = entities.Flights
            .ToArray()
            .SelectMany(f => f.Bookings.Where(b => b.PassengerEmail == email)
                .Select(brm => new BookingRm(f.Id,
                    f.Airline,
                    f.Price,
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    brm.NumberOfSeats,
                    brm.PassengerEmail)));

        return Ok(bookings);
    }
}