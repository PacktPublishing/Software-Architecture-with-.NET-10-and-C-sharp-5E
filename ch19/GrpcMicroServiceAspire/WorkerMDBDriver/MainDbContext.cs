using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerMDBDriver.Models;

namespace DBDriver
{
    internal class MainDbContext : DbContext, IUnitOfWork
    {
        public DbSet<DayTotal> DayTotals { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public MainDbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder
        builder)
        {
        }
        #region IUnitOfWork Implementation
        public async Task<bool> SaveEntitiesAsync()
        {
            try
            {
                return await SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries) await entry.ReloadAsync();
                throw new ConcurrencyException(ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ConstraintViolationException(ex);
            }
        }
        public async Task StartAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            await Database.BeginTransactionAsync(isolationLevel);
        }
        public Task CommitAsync()
        {
            Database.CommitTransaction();
            return Task.CompletedTask;
        }
        public Task RollbackAsync()
        {
            Database.RollbackTransaction();
            return Task.CompletedTask;
        }
        #endregion

    }
}
