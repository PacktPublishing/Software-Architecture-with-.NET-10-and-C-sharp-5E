using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB.Models;
using PackagesManagementDomainServices.IRepositories;
using DBDriver;
using PackagesManagementDomainModel.Aggregates;

namespace PackagesManagementDB.Repositories
{
    public class PackageEventRepository : IPackageEventRepository
    {
        private MainDbContext _context;
        public PackageEventRepository(IUnitOfWork iow)
        {
            _context = (MainDbContext)iow;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<PackageEventAggregate>> GetFirstNAsync(int n)
        {
            return (await _context.PackageEvents
                .OrderBy(m => m.Id)
                .Take(n)
                .ToListAsync())
                .Select(m => new PackageEventAggregate(m)).ToList();
        }

        public PackageEventAggregate New(PackageEventType type, int id, long oldVersion, long? newVersion=null, decimal price=0)
        {
            var model = new PackageEvent
            {
                Type = type,
                PackageId = id,
                OldVersion = oldVersion,
                NewVersion = newVersion,
                NewPrice = price
            };
            _context.PackageEvents.Add(model);
            return new PackageEventAggregate(model);
        }
    }
}
