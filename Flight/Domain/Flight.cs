namespace Domain;

public class Flight
{
    public int RemainingNumberOfSeats { get; set; }

    private readonly List<Booking> _bookingList = new();
    public IEnumerable<Booking> BookingList => _bookingList;

    public Flight(int seatCapacity)
    {
        RemainingNumberOfSeats = seatCapacity;
    }

    public object? Book(string passengerEmail, int numberOfSeats)
    {
        if (numberOfSeats > RemainingNumberOfSeats)
        {
            return new OverBookingError();
        }

        RemainingNumberOfSeats -= numberOfSeats;
        _bookingList.Add(new Booking(passengerEmail, numberOfSeats));

        return null;
    }

    public object? CancelBooking(string passengerEmail, int numberOfSeats)
    {
        if (_bookingList.All(booking => booking.PassengerEmail != passengerEmail))
        {
            return new BookingNotFoundError();
        }

        RemainingNumberOfSeats += numberOfSeats;
        return null;
    }
}