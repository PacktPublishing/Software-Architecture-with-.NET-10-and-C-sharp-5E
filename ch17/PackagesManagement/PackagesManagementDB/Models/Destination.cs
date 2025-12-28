
using PackagesManagementDomainModel.Aggregates;
using System.ComponentModel.DataAnnotations;

namespace PackagesManagementDB.Models
{
    
    public class Destination: IDestination
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Name { get; set; } = null!;
        [MaxLength(128)]
        public string Country { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Package> Packages { get; set; } = null!;
        
    }
}
