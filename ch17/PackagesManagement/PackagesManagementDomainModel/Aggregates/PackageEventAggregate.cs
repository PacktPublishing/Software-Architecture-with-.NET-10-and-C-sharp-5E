using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomainModel.Aggregates
{
    public enum PackageEventType { Deleted, CostChanged }
    public  class PackageEventAggregate(IPackageEvent state): Entity<long>
    {
        public override long Id => state.Id;
        public PackageEventType Type => state.Type;
        public int PackageId => state.PackageId;
        public decimal NewPrice => state.NewPrice;
        public long? OldVersion => state.OldVersion;
        public long? NewVersion => state.NewVersion;
    }
}
