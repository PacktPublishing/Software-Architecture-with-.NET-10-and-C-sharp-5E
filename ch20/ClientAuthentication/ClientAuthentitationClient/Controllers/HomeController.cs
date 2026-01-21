using ClientAuthentitationClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientAuthentitationClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> IndexAsync([FromServices] IHttpClientFactory cFactory)
        {
            var client = cFactory.CreateClient("clientCertificateClient");
            var response = await client.GetAsync("WeatherForecast");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
