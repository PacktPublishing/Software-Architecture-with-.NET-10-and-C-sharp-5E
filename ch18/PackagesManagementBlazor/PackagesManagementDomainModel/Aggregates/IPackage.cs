using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public interface IPackage
    {
        int Id { get; set; }
        string Name { get; set; } 
        string? Description { get; set; }
        decimal Price { get; set; }
        int DurationInDays { get; set; }
        DateTime? StartValidityDate { get; set; }
        DateTime? EndValidityDate { get; set; }
        int DestinationId { get; set; }
        long EntityVersion { get; set; }

    }
}
