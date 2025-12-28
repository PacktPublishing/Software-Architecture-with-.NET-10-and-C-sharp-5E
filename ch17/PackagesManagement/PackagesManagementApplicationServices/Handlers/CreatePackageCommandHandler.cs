using DDD.ApplicationLayer;
using PackagesManagementApplicationServices.Commands;
using PackagesManagementDomainServices.IRepositories;


namespace PackagesManagementApplicationServices.Handlers
{
    public class CreatePackageCommandHandler(IPackageRepository repo) : ICommandHandler<CreatePackageCommand>
    {
        
        public async Task  HandleAsync(CreatePackageCommand command)
        {
            var model= repo.New(command.Name, command.Description, command.Price, 
                command.DurationInDays, command.StartValidityDate, command.EndValidityDate, 
                command.DestinationId);
            
            await repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
