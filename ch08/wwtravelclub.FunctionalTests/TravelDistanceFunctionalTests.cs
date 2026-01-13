using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;

namespace wwtravelclub.FunctionalTests;

public class TravelDistanceFunctionalTests : IClassFixture<WebApplicationFactory<ch08.Startup>>
{
    private readonly WebApplicationFactory<ch08.Startup> _factory;

    public TravelDistanceFunctionalTests(WebApplicationFactory<ch08.Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_TravelDistancePage_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/TravelDistance");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Travel Distance", content);
        Assert.Contains("From City:", content);
        Assert.Contains("To City:", content);
        Assert.Contains("Calculate Distance", content);
    }

    [Fact]
    public async Task Get_TravelDistancePage_ContainsCityOptions()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/TravelDistance");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("New York", content);
        Assert.Contains("Los Angeles", content);
        Assert.Contains("London", content);
        Assert.Contains("Paris", content);
    }
}
