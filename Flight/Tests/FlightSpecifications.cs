using Domain;
using FluentAssertions;

namespace Tests;

public class FlightSpecifications
{
    [Fact]
    public void Booking_reduces_the_number_of_seats()
    {
        var flight = new Flight(seatCapacity: 3);

        flight.Book("james@test.com", 1);

        flight.RemainingNumberOfSeats.Should().Be(2);
    }

    [Fact]
    public void Avoids_overbooking()
    {
        // Given
        var flight = new Flight(seatCapacity: 3);
        
        // When
        var error = flight.Book("james@test.com", 4);
        
        // Then
        error.Should().BeOfType<OverBookingError>();
    }

    [Fact]
    public void Books_flights_successfully()
    {
        var flight = new Flight(seatCapacity: 3);
        var error = flight.Book("james@test.com", 1);
        error.Should().BeNull();
    }
}