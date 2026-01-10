using DDD.DomainLayer;
using Grpc.Core;
using WorkerMDomainServices.IRepositories;

namespace GrpcMicroService.Services
{
    public class CounterService: Counter.CounterBase
    {
        private readonly IQueueItemRepository _queue;
        private readonly IUnitOfWork _uow;
        public CounterService(IQueueItemRepository queue,
            IUnitOfWork uow)
        {
            _queue = queue;
            _uow = uow;
        }
        public override async Task<CountingReply> Count(
            CountingRequest request,
            ServerCallContext context)
        {
            _queue.Enqueue(
              new WorkerMDomainServices.DTOs.PurchaseInfoDTO
              {
                  Cost = request.Cost,
                  MessageId = Guid.Parse(request.Id),
                  Location = request.Location,
                  PurchaseTime =
                   request.PurchaseTime.ToDateTimeOffset(),
                  Time = request.Time.ToDateTimeOffset()
              });
            await _uow.SaveEntitiesAsync();
            return new CountingReply { };
        }

    }
}
