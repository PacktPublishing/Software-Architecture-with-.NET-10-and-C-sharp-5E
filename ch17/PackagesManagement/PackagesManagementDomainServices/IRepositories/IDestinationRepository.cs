using DDD.DomainLayer;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackagesManagementDomainServices.IRepositories
{
    public interface IDestinationRepository:IRepository
    {
        Task<IList<KeyDisplayPair<int, string>>> GetAllShort();
        Task<DestinationAggregate?> GetAsync(int id);
        DestinationAggregate New(string Name, string Country, string? description=null);

    }
}
