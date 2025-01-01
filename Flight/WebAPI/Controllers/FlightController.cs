using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers.Dtos;
using WebAPI.Data;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Entities.Errors;
using WebAPI.ReadModels;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController(ILogger<FlightController> logger, Entities entities) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(FlightRm), StatusCodes.Status200OK)]
    [HttpGet("{flightId}")]
    public ActionResult<FlightRm?> Find(Guid flightId)
    {
        var flight = entities.Flights.FirstOrDefault(f => f.Id == flightId);
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
    public ActionResult<IEnumerable<FlightRm>> Search([FromQuery] FlightSearchParameters @params)
    {
        Console.WriteLine(@params.ToString());

        IQueryable<Flight> flights = entities.Flights;

        if (!string.IsNullOrWhiteSpace(@params.From))
        {
            flights = flights.Where(f => f.Departure.Place.Contains(@params.From));
        }

        if (!string.IsNullOrWhiteSpace(@params.Destination))
        {
            flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));
        }

        if (!string.IsNullOrWhiteSpace(@params.Destination))
        {
            flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));
        }

        if (@params.FromDate != null)
        {
            flights = flights.Where(f => f.Departure.Time >= @params.FromDate);
        }

        if (@params.ToDate != null)
        {
            flights = flights.Where(f => f.Departure.Time <= @params.ToDate);
        }

        if (@params.NumberOfPassengers > 0)
        {
            flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassengers);
        }

        var flightRmList = flights
            .Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
                flight.RemainingNumberOfSeats)).ToList();

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
        var flight = entities.Flights.FirstOrDefault(f => f.Id == dto.FlightId);
        if (flight == null)
        {
            return NotFound();
        }

        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
        if (error is OverBookError)
        {
            return Conflict(new { message = "Not enough seats" });
        }

        try
        {
            entities.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return Conflict(new { message = "An error occurred while processing your request. Please try again." });
        }

        return CreatedAtAction(nameof(Find), new { flightId = dto.FlightId }, null);
    }
}