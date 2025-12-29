using DDD.ApplicationLayer;
using PackagesManagementApplicationServices.Commands;
using PackagesManagementDomainServices.IRepositories;

namespace PackagesManagement.Handlers
{
    public class DeletePackageCommandHandler(IPackageRepository repo, EventMediator mediator) : ICommandHandler<DeletePackageCommand>
    {
        
        public async Task HandleAsync(DeletePackageCommand command)
        {
            var deleted = await repo.Delete(command.PackageId);
            if (deleted is not null)
                await mediator.TriggerEvents(deleted.DomainEvents);
            await repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
