using DDD.ApplicationLayer;
using PackagesManagementDomainServices.DTOs;

namespace PackagesManagementApplicationServices.Queries
{
    public interface IPackagesListQuery: IQuery
    {
        Task<IList<PackageInfosDTO>> GetAllPackages();
        
    }
}
