namespace Domain;

public class Booking
{
    public string PassengerEmail { get; set; }
    public int NumberOfSeats { get; set; }

    public Booking(string passengerEmail, int numberOfSeats)
    {
        PassengerEmail = passengerEmail;
        NumberOfSeats = numberOfSeats;
    }
}