using PackagesManagementDomainServices.DTOs;
using PackagesManagementDomainServices.IRepositories;
using System.Collections.ObjectModel;

namespace PackagesManagementApplicationServices.Queries
{
    public class PackagesListByLocationQuery(IPackageRepository repo) : 
        IPackagesListByLocationQuery
    {
        public async Task<IReadOnlyCollection<PackageInfosDTO>> GetPackagesOf(string search)
        {
            return new ReadOnlyCollection<PackageInfosDTO> (
                await repo.GetInfoByDestination(search));
        }
    }
}
