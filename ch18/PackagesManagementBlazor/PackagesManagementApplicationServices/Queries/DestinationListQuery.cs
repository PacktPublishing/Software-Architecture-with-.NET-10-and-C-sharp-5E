using PackagesManagementDomainServices.DTOs;
using PackagesManagementDomainServices.IRepositories;

namespace PackagesManagementApplicationServices.Queries
{
    public class DestinationListQuery(IDestinationRepository repo) : 
        IDestinationListQuery
    {
        public async Task<IEnumerable<KeyDisplayPair<int, string>>> AllDestinations()
        {
            return await repo.GetAllShort();
        }
    }
}
