using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagementDB.Models;
using PackagesManagementDomainServices.IRepositories;
using DBDriver;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainServices.DTOs;

namespace PackagesManagementDB.Repositories
{
    public class DestinationRepository : IDestinationRepository

    {
        private MainDbContext _context;
        public DestinationRepository(IUnitOfWork iow)
        {
            _context = (MainDbContext)iow;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<DestinationAggregate?> GetAsync(int id)
        {
            var entity = await _context.Destinations.Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            if (entity == null) return null;
            else return new DestinationAggregate(entity);
        }

        public DestinationAggregate New(string name, string country, string? description = null)
        {
            var model = new Destination { 
                Name = name,
                Country = country,
                Description = description
            };
            _context.Destinations.Add(model);
            return new DestinationAggregate(model);
        }

        public async Task<IList<KeyDisplayPair<int, string>>> GetAllShort()
        {
            return (await _context.Destinations.Select(m => new KeyDisplayPair<int, string>
            {
                Key = m.Id,
                Display = m.Name
            })
           .OrderBy(m => m.Display)
           .ToListAsync());
        }
    }
}
