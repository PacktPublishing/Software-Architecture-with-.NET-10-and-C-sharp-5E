using DBDriver;
using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerMDBDriver.Models;
using WorkerMDomainModel.Models;
using WorkerMDomainServices.IRepositories;

namespace WorkerMDBDriver.Repositories
{
    internal class DayTotalRepository : IDayTotalRepository
    {
        private readonly MainDbContext ctx;
        public DayTotalRepository(IUnitOfWork uw)
        {
            ctx = (MainDbContext)uw;
        }
        public async Task<DayTotalAggregate?> Get(DateTimeOffset day)
        {
            var state = await ctx.DayTotals.Where(m => m.Id == day).FirstOrDefaultAsync();
            if (state == null) return null;
            else return new DayTotalAggregate(state);
        }

        public DayTotalAggregate New(DateTimeOffset day)
        {
            var state = new DayTotal() { Id = day} ;
            ctx.DayTotals.Add(state);
            return new DayTotalAggregate(state);
        }
    }
}
