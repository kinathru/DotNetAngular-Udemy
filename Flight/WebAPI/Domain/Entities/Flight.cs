using WebAPI.Domain.Entities.Errors;

namespace WebAPI.Domain.Entities;

public record Flight(
    Guid Id,
    string Airline,
    string Price,
    TimePlace Departure,
    TimePlace Arrival,
    int RemainingNumberOfSeats)
{
    public IList<Booking> Bookings = [];
    public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

    public object? MakeBooking(string passengerEmail, byte numberOfSeats)
    {
        if (RemainingNumberOfSeats < numberOfSeats)
        {
            return new OverBookError();
        }

        Bookings.Add(new Booking(passengerEmail, numberOfSeats));
        RemainingNumberOfSeats -= numberOfSeats;
        
        return null;
    }
}