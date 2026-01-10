using DBDriver;
using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDBDriver.Models;
using WorkerMDomainModel.Models;
using WorkerMDomainServices.DTOs;
using WorkerMDomainServices.IRepositories;

namespace WorkerMDBDriver.Repositories
{
    internal class QueueItemRepository : IQueueItemRepository
    {
        private readonly MainDbContext ctx;
        public QueueItemRepository(IUnitOfWork uw)
        {
            ctx = (MainDbContext)uw;
        }
        public async Task Dequeue(IEnumerable<QueueItemAggregate> items)
        {
            var states = items.Select(async m => await ctx.QueueItems.FindAsync(new object[m.Id])).ToList();
            foreach (var tItem in states)
            {
                var item = await tItem;
                if(item != null)
                    ctx.QueueItems.Remove(item);
            }
            
        }

        public QueueItemAggregate Enqueue(PurchaseInfoDTO mssageInfo)
        {
            
            var toAdd = new QueueItem
            {
                Cost= mssageInfo.Cost,
                ExtractionTime= DateTimeOffset.MinValue,
                MessageId= mssageInfo.MessageId,
                Location= mssageInfo.Location,
                Time= mssageInfo.Time,
                PurchaseTime= mssageInfo.PurchaseTime,
            };
            ctx.QueueItems.Add(toAdd);
            return new QueueItemAggregate(toAdd);
        }

        public async Task<IList<QueueItemAggregate>> Top(int n, int minutesBlocked)
        {
            var limit = DateTimeOffset.Now.AddMinutes(-minutesBlocked);
            var states = await ctx.QueueItems
                .OrderByDescending(m => m.Time)
                .Where(m => m.ExtractionTime < limit)
                .ToListAsync();
            return states.Select(m => new QueueItemAggregate(m)).ToList();
        }
    }
}
