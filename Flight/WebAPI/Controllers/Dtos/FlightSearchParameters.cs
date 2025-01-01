using System.ComponentModel;

namespace WebAPI.Controllers.Dtos;

public record FlightSearchParameters(
    DateTime? FromDate,
    DateTime? ToDate,
    string? From,
    string? Destination,
    [DefaultValue(1)] int? NumberOfPassengers)
{
    public override string ToString()
    {
        return
            $"FlightSearchParameters [{nameof(FromDate)}: {FromDate}, {nameof(ToDate)}: {ToDate}, {nameof(From)}: {From}, {nameof(Destination)}: {Destination}, {nameof(NumberOfPassengers)}: {NumberOfPassengers}]";
    }
}