using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Entities.Errors;
using WebAPI.ReadModels;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController(ILogger<FlightController> logger) : ControllerBase
{
    private static readonly List<Flight> DummyFlightRms =
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

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(FlightRm), StatusCodes.Status200OK)]
    [HttpGet("{flightId}")]
    public ActionResult<FlightRm?> Find(Guid flightId)
    {
        var flight = DummyFlightRms.FirstOrDefault(f => f.Id == flightId);
        if (flight == null)
        {
            return NotFound();
        }

        var flightReadModel = new FlightRm(
            flight.Id,
            flight.Airline,
            flight.Price,
            new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
            new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
            flight.RemainingNumberOfSeats);

        return Ok(flightReadModel);
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IEnumerable<FlightRm>), StatusCodes.Status200OK)]
    [HttpGet]
    public ActionResult<IEnumerable<FlightRm>> Search()
    {
        var flightRmList = DummyFlightRms.Select(flight =>
        {
            var flightReadModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
                flight.RemainingNumberOfSeats);
            return flightReadModel;
        }).ToList();

        return Ok(flightRmList);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Book(BookDto dto)
    {
        System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");
        var flight = DummyFlightRms.FirstOrDefault(f => f.Id == dto.FlightId);
        if (flight == null)
        {
            return NotFound();
        }

        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
        if (error is OverBookError)
        {
            return Conflict(new { message = "Not enough seats" });
        }

        return CreatedAtAction(nameof(Find), new { flightId = dto.FlightId }, null);
    }
}