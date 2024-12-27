namespace Domain;

public class Flight
{
    public int RemainingNumberOfSeats { get; set; }
    public List<Booking> BookingList { get; set; } = new();

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
        BookingList.Add(new Booking(passengerEmail, numberOfSeats));
        
        return null;
    }
}