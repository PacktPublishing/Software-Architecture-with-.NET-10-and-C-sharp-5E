using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB.Models;
using RoutesPlanningDomainLayer.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDriver
{
    internal class MainDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<PackageEvent> PackageEvents { get; set; }
        public MainDbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder
        builder)
        {
            builder.Entity<Destination>()
                .HasMany(m => m.Packages)
                .WithOne(m => m.MyDestination)
                .HasForeignKey(m => m.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            new DestinationConfiguration()
                .Configure(builder.Entity<Destination>());
            new PackageConfiguration()
                .Configure(builder.Entity<Package>());
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
        public async Task CommitAsync()
        {
            await Database.CommitTransactionAsync();
        }
        public async Task RollbackAsync()
        {
            await Database.RollbackTransactionAsync();
        }
        #endregion

    }
}
