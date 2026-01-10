using DDD.ApplicationLayer;
using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WorkerMApplicationServices.Commands;
using WorkerMDomainServices.IRepositories;

namespace WorkerMApplicationServices.Handlers
{
    internal class PurchaseCommandHandler(
        IPurchaseRepository purchaseRepo, 
        IDayTotalRepository dayTotalRepo,
        IUnitOfWork uow) : ICommandHandler<PurchaseCommand>
    {
        public async Task HandleAsync(PurchaseCommand command)
        {
            var purchase=command.Purchase;
            await uow.StartAsync();
            try
            {
                var existing = await purchaseRepo.Get(purchase.MessageId);
                if (existing != null)
                {
                    await uow.RollbackAsync();
                    return;
                }
                else
                {
                    var day = await dayTotalRepo.Get(purchase.PurchaseTime);
                    if (day == null) day = dayTotalRepo.New(purchase.PurchaseTime);
                    day.Add(purchase.Cost);
                    purchaseRepo.New(purchase);
                    await uow.SaveEntitiesAsync();
                    await uow.CommitAsync();
                }
            }
            catch
            {
                await uow.RollbackAsync();
                throw;
            }

        }
    }
}
