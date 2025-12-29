using PackagesManagementDomainModel.Aggregates;


namespace PackagesManagementDB.Models
{
    public class PackageEvent: IPackageEvent
    {
        public long Id { get; set; }
        public PackageEventType Type { get; set; }
        public int PackageId { get; set; }
        public decimal NewPrice { get; set; }
        public long? OldVersion { get; set; }
        public long? NewVersion { get; set; }
    }
}
