using Domain;
using FluentAssertions;

namespace Tests;

public class FlightSpecifications
{
    [Theory]
    [InlineData(3,1,2)]
    [InlineData(6,3,3)]
    [InlineData(10,6,4)]
    public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
    {
        var flight = new Flight(seatCapacity: seatCapacity);

        flight.Book("james@test.com", numberOfSeats);

        flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
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