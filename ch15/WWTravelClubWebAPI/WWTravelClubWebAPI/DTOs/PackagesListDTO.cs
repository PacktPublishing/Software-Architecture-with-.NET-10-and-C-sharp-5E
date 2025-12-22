using System;

namespace WWTravelClubWebAPI.DTOs;

public record PackagesListDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public DateTime? StartValidityDate { get; set; }
    public DateTime? EndValidityDate { get; set; }
    public required string DestinationName { get; set; }
    public int DestinationId { get; set; }
}
