using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers.Dtos;

public record BookDto(
    [Required] Guid FlightId,
    [Required] [EmailAddress] string PassengerEmail,
    [Required] [Range(1, 254)] byte NumberOfSeats);