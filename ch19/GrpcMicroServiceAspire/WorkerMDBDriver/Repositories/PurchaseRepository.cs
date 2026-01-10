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
    internal class PurchaseRepository : IPurchaseRepository
    {
        private readonly MainDbContext ctx;
        public PurchaseRepository(IUnitOfWork uw)
        {
            ctx = (MainDbContext)uw;
        }
        public async Task<PurchaseAggregate?> Get(Guid messageId)
        {
            var state = await ctx.Purchases.Where(m => m.Id == messageId).FirstOrDefaultAsync();
            if (state == null) return null;
            else return new PurchaseAggregate(state);
        }

        public PurchaseAggregate New(PurchaseInfoDTO message)
        {
            var state = new Purchase 
            { 
                Cost = message.Cost,
                Location = message.Location,
                PurchaseTime=message.PurchaseTime,
                Time=message.Time
            };
            ctx.Purchases.Add(state);
            return new PurchaseAggregate(state);
        }
    }
}
