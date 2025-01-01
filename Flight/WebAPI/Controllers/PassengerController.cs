using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;
using WebAPI.Domain.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
    private static readonly List<Passenger> Passengers = new();

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<NewPassengerDto?> Register(NewPassengerDto dto)
    {
        Passengers.Add(new Passenger(dto.Email, dto.FirstName, dto.LastName, dto.Gender));
        System.Diagnostics.Debug.WriteLine(Passengers.Count);
        return CreatedAtAction(nameof(Find), new { email = dto.Email }, dto);
    }

    [HttpGet("{email}")]
    public ActionResult<PassengerRm?> Find(string email)
    {
        var passenger = Passengers.FirstOrDefault(p => p.Email == email);
        if (passenger == null)
        {
            return NotFound();
        }

        return Ok(new PassengerRm(passenger.Email, passenger.FirstName, passenger.LastName, passenger.Gender));
    }
}