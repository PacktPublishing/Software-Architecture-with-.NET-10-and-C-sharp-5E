using PackagesManagementBlazor.Shared;
using System.Net.Http.Json;

namespace PackagesManagementMAUI.Services
{
    public class PackagesClient(HttpClient client)
    {
        public async Task<IEnumerable<PackageInfosViewModel>>
            GetByLocationAsync(string location)
        {
            var result =
                await client.GetFromJsonAsync<PackagesListViewModel>
                    ("Packages/" + Uri.EscapeDataString(location));
            return result?.Items??new List<PackageInfosViewModel>();
        }
    }
}
