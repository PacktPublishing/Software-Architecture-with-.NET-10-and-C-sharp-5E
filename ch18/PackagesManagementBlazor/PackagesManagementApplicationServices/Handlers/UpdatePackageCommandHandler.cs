using DDD.ApplicationLayer;
using PackagesManagementApplicationServices.Commands;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainServices.IRepositories;
using RoutesPlanningDomainLayer.Tools;


namespace PackagesManagement.Handlers
{
    public class UpdatePackageCommandHandler(IPackageRepository repo, EventMediator mediator) : ICommandHandler<UpdatePackageCommand>
    {
        
        public async Task HandleAsync(UpdatePackageCommand command)
        {
            bool done = false;
            PackageAggregate? model;
            while (!done)
            {
                try
                {
                    model = await repo.GetAsync(command.Id);
                    if (model is null) return;
                    model.Name= command.Name;
                    model.Description = command.Description;
                    model.DurationInDays = command.DurationInDays;
                    model.Price = command.Price;
                    model.StartValidityDate = command.StartValidityDate;
                    model.EndValidityDate = command.EndValidityDate;

                    await mediator.TriggerEvents(model.DomainEvents);
                    await repo.UnitOfWork.SaveEntitiesAsync();
                    done = true;
                }
                catch (ConcurrencyException)
                {
                    //add here some logging
                }
            }
        }
    }
}
