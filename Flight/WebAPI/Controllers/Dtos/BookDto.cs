﻿namespace WebAPI.Controllers.Dtos;

public record BookDto(Guid FlightId, string PassengerEmail, byte NumberOfSeats);