
using PackagesManagementDomainModel.Aggregates;
using System.ComponentModel.DataAnnotations;
namespace PackagesManagementDB.Models
{
    public class Package : IPackage
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public required string Name { get; set; } 
        [MaxLength(128)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public Destination MyDestination { get; set; } = null!;
        [ConcurrencyCheck]
        public long EntityVersion { get; set; }

        public int DestinationId { get; set; } 
    }
}
