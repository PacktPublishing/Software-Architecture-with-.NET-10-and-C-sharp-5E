using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public interface IPackageEvent
    {
        long Id { get; set; }
        PackageEventType Type { get; set; }
        int PackageId { get; set; }
        decimal NewPrice { get; set; }
        long? OldVersion { get; set; }
        long? NewVersion { get; set; }
    }
}
