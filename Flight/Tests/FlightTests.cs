﻿using Domain.Tests;
using FluentAssertions;

namespace Tests;

public class FlightTests
{
    [Fact]
    public void Booking_reduces_the_number_of_seats()
    {
        var flight = new Flight(seatCapacity: 3);

        flight.Book("james@test.com", 1);

        flight.RemainingNumberOfSeats.Should().Be(2);
    }
}