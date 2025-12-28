using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public interface IDestination
    {
        int Id { get; set; }
        string Name { get; set; }
        string Country { get; set; }
        string? Description { get; set; }
    }
}
