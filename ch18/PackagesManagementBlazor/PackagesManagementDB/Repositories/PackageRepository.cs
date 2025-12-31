using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB.Models;
using DBDriver;
using PackagesManagementDomainServices.IRepositories;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainModel.Events;
using PackagesManagementDomainServices.DTOs;

namespace PackagesManagementDB.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly MainDbContext _context;
        public PackageRepository(IUnitOfWork iow)
        {
            _context = (MainDbContext)iow;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<PackageAggregate?> GetAsync(int id)
        {
            var model = await _context.Packages.Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            return model == null ? null : new PackageAggregate(model);
        }
        public async Task<PackageAggregate?> Delete(int id)
        {
            var model = await _context.Packages.Where(m => m.Id == id)
                .FirstOrDefaultAsync(); 
            if (model is not Package package) return null;
            _context.Packages.Remove(package);
            var result=new PackageAggregate(package);
            result.AddDomainEvent(
               new PackageDeleteEvent(
                    model.Id, package.EntityVersion));
            return result;

        }
        public PackageAggregate New(string name, string description, decimal price,
            int durationInDays, DateTime? startValidityDate, DateTime? endValidityDate, int DestinationId)
        {
            var model = new Package() {
                EntityVersion=1, Name=name, Description=description,
                Price=price, DurationInDays=durationInDays, StartValidityDate=startValidityDate, 
                EndValidityDate=endValidityDate, DestinationId=DestinationId
            };
            _context.Packages.Add(model);
            return new PackageAggregate(model);
        }

        public async Task<IList<PackageInfosDTO>> GetAllInfo()
        {
            return await _context.Packages.Select(m => new PackageInfosDTO
            {
                StartValidityDate = m.StartValidityDate,
                EndValidityDate = m.EndValidityDate,
                Name = m.Name,
                DurationInDays = m.DurationInDays,
                Id = m.Id,
                Price = m.Price,
                DestinationName = m.MyDestination.Name,
                DestinationId = m.DestinationId
            })
                .OrderByDescending(m => m.EndValidityDate)
                .ToListAsync();
        }

        public async Task<IList<PackageInfosDTO>> GetInfoByDestination(string search)
        {
            return await _context.Packages
            .Where(m => m.MyDestination.Name.Contains(search))
            .Select(m => new PackageInfosDTO
            {
                StartValidityDate = m.StartValidityDate,
                EndValidityDate = m.EndValidityDate,
                Name = m.Name,
                DurationInDays = m.DurationInDays,
                Id = m.Id,
                Price = m.Price,
                DestinationName = m.MyDestination.Name,
                DestinationId = m.DestinationId
            })
            .OrderByDescending(m => m.EndValidityDate)
            .ToListAsync();

        }
    }
}
