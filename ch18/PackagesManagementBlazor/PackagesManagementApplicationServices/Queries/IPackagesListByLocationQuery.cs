using DDD.ApplicationLayer;
using PackagesManagementDomainServices.DTOs;
using System.Collections.ObjectModel;


namespace PackagesManagementApplicationServices.Queries
{
    public interface IPackagesListByLocationQuery: IQuery
    {
        Task<IReadOnlyCollection<PackageInfosDTO>>
            GetPackagesOf(string location);

    }
}
