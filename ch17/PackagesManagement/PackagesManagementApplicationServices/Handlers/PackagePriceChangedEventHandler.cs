using DDD.ApplicationLayer;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainModel.Events;
using PackagesManagementDomainServices.IRepositories;


namespace PackagesManagement.Handlers
{
public class PackagePriceChangedEventHandler(IPackageEventRepository repo) :
    IEventHandler<PackagePriceChangedEvent>
{
        
    public Task HandleAsync(PackagePriceChangedEvent ev)
    {
        repo.New(PackageEventType.CostChanged, ev.PackageId, 
            ev.OldVersion, ev.NewVersion, ev.NewPrice);
        return Task.CompletedTask;
    }
}
}
