using DDD.ApplicationLayer;
using PackagesManagementDomainServices.DTOs;


namespace PackagesManagementApplicationServices.Queries
{
    public interface IDestinationListQuery: IQuery
    {
        Task<IEnumerable<KeyDisplayPair<int, string>>> AllDestinations();
    }
}
