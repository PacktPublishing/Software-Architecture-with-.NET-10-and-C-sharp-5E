using Microsoft.AspNetCore.Mvc;
using PackagesManagementApplicationServices.Queries;
using PackagesManagementBlazor.Shared;
using System.Collections.Immutable;

namespace PackagesManagementBlazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        [HttpGet("{search}")]
        public async Task<PackagesListViewModel>
        GetAsync(string search,
            [FromServices] IPackagesListByLocationQuery query)
        {
            return new PackagesListViewModel
            {
                Items = (await query.GetPackagesOf(search))
                 .Select(m => new PackageInfosViewModel
                 {
                     Id = m.Id,
                     Name = m.Name,
                     DestinationName = m.DestinationName,
                     DestinationId = m.DestinationId,
                     Price = m.Price,
                     DurationInDays = m.DurationInDays,
                     StartValidityDate = m.StartValidityDate,
                     EndValidityDate = m.EndValidityDate

                 }).ToImmutableList()
            };
        }

    }
}
