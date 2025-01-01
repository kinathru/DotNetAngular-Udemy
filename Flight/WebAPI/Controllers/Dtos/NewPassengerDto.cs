using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers.Dtos;

public record NewPassengerDto(
    [Required]
    [EmailAddress]
    [StringLength(100, MinimumLength = 3)]
    string Email,
    [Required]
    [MinLength(2)]
    [MaxLength(35)]
    string FirstName,
    [Required] string LastName,
    [Required] bool Gender);