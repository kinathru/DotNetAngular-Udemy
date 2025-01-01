using WebAPI.Domain.Entities;

namespace WebAPI.Data;

public class Entities
{
    public readonly List<Passenger> Passengers = new();

    public readonly List<Flight> Flights = [];
}