using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;
using WebAPI.Data;
using WebAPI.Domain.Entities.Errors;
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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Cancel(BookDto dto)
    {
        var flight = entities.Flights.Find(dto.FlightId);
        var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

        if (error == null)
        {
            entities.SaveChanges();
            return NoContent();
        }

        if (error is NotFoundError)
        {
            return NotFound();
        }

        throw new Exception(
            $"The error of type : {error.GetType().Name} occured while canceling the booking made by {dto.PassengerEmail}");
    }
}