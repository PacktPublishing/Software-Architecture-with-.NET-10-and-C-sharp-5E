using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDomainModel.Models;
using WorkerMDomainServices.DTOs;

namespace WorkerMDomainServices.IRepositories
{
    public interface IQueueItemRepository: IRepository
    {
        public Task<IList<QueueItemAggregate>> Top(int n, int minutesBlocked);
        public Task Dequeue(IEnumerable<QueueItemAggregate> items);
        public QueueItemAggregate Enqueue(PurchaseInfoDTO mssageInfo);
    }
}
