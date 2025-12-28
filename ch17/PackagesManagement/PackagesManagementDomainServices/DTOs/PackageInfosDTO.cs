using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainServices.DTOs
{
    public class PackageInfosDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartValidityDate { get; set; }
        public DateTime? EndValidityDate { get; set; }
        public string DestinationName { get; set; } = null!;
        public int DestinationId { get; set; }
    }
        
}
