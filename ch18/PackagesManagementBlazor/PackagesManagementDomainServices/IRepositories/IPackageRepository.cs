using DDD.DomainLayer;
using PackagesManagementDomainModel.Aggregates;
using PackagesManagementDomainServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackagesManagementDomainServices.IRepositories
{
    public interface IPackageRepository: IRepository
    {
        Task<IList<PackageInfosDTO>> GetAllInfo();
        Task<IList<PackageInfosDTO>> GetInfoByDestination(string search);

        Task<PackageAggregate?> GetAsync(int id);
        PackageAggregate New(string name, string description, decimal price,
            int durationInDays, DateTime? startValidityDate, DateTime? endValidityDate, int DestinationId);
        Task<PackageAggregate?> Delete(int id);
    }
}
