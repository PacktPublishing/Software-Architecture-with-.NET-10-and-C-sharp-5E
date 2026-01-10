using DDD.ApplicationLayer;
using DDD.DomainLayer;
using WorkerMApplicationServices.Commands;
using WorkerMDBDriver.Models;
using WorkerMDomainModel.Models;
using WorkerMDomainServices.DTOs;
using WorkerMDomainServices.IRepositories;

namespace GrpcMicroService.HostedServices
{
    public class ProcessPurchases: BackgroundService
    {
        private const int MinuteBlocked = 3;
        readonly IServiceProvider _services;
        public ProcessPurchases(IServiceProvider services)
        {
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool queueEmpty = false;
            while (!stoppingToken.IsCancellationRequested)
            {
                while (!queueEmpty && !stoppingToken.IsCancellationRequested)
                {
                    using (var scope = _services.CreateScope())
                    {
                        IQueueItemRepository queue = scope.ServiceProvider.GetRequiredService<IQueueItemRepository>();
                        IUnitOfWork uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        var toProcess = await queue.Top(10, MinuteBlocked);
                        if (toProcess.Count > 0)
                        {
                            Task<QueueItemAggregate>[] tasks = new Task<QueueItemAggregate>[toProcess.Count];
                            for (int i = 0; i < tasks.Length; i++)
                            {
                                var toExecute = async () =>
                                {
                                    QueueItemAggregate item = toProcess[i];
                                    using (var sc = _services.CreateScope())
                                    {
                                        var handler = sc.ServiceProvider.GetRequiredService<ICommandHandler<PurchaseCommand>>();
                                        try
                                        {
                                            await handler.HandleAsync(new PurchaseCommand(new PurchaseInfoDTO
                                            {
                                                Cost = item.Cost,
                                                Location = item.Location,
                                                MessageId = item.MessageId,
                                                PurchaseTime = item.PurchaseTime,
                                                Time = item.Time
                                            }));
                                            return item;
                                        }
                                        catch
                                        {
                                            return null;
                                        }
                                    } 
                                };
                                tasks[i] = toExecute();
                            }
                            await Task.WhenAll(tasks);
                            var processed = tasks.Select(m => m.Result).Where(m => m != null).ToList();
                            if (processed.Count > 0)
                            {
                                await queue.Dequeue(processed);
                                await uow.SaveEntitiesAsync();
                            }
                            
                        }
                        else queueEmpty = true;
                    }
                }
                await Task.Delay(100, stoppingToken);
                queueEmpty = false;
            }
        }
    }
}
