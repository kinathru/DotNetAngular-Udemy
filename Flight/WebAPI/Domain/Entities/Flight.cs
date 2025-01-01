using WebAPI.Domain.Entities.Errors;

namespace WebAPI.Domain.Entities;

public class Flight
{
    public Guid Id { get; set; }
    public string Airline { get; set; }
    public string Price { get; set; }
    public TimePlace Departure { get; set; }
    public TimePlace Arrival { get; set; }
    public int RemainingNumberOfSeats { get; set; }

    public IList<Booking> Bookings = [];

    public Flight()
    {
    }

    public Flight(Guid id, string airline, string price, TimePlace departure, TimePlace arrival,
        int remainingNumberOfSeats)
    {
        Id = id;
        Airline = airline;
        Price = price;
        Departure = departure;
        Arrival = arrival;
        RemainingNumberOfSeats = remainingNumberOfSeats;
    }

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

    public object? CancelBooking(string passengerEmail, byte numberOfSeats)
    {
        var booking = Bookings.SingleOrDefault(b =>
            string.Equals(b.PassengerEmail, passengerEmail, StringComparison.CurrentCultureIgnoreCase) &&
            b.NumberOfSeats == numberOfSeats);

        if (booking == null)
        {
            return new NotFoundError();
        }

        Bookings.Remove(booking);
        RemainingNumberOfSeats -= booking.NumberOfSeats;
        
        return null;
    }
}