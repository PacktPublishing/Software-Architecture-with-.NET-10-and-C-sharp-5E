using DDD.ApplicationLayer;
using PackagesManagementDomainServices.IRepositories;
using PackagesManagementDomainModel.Events;
using PackagesManagementDomainModel.Aggregates;

namespace PackagesManagement.Handlers
{
    public class PackageDeleteEventHandler(IPackageEventRepository repo) :
        IEventHandler<PackageDeleteEvent>
    {
        
        public Task HandleAsync(PackageDeleteEvent ev)
        {
            repo.New(PackageEventType.Deleted, ev.PackageId, ev.OldVersion);
            return Task.CompletedTask;
        }
    }
}
