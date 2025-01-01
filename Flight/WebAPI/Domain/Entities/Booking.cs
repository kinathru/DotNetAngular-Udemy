namespace WebAPI.Domain.Entities;

public record Booking(
    string PassengerEmail,
    byte NumberOfSeats);