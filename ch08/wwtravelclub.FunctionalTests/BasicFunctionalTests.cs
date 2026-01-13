using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace wwtravelclub.FunctionalTests;

public class BasicFunctionalTests : IClassFixture<WebApplicationFactory<ch08.Startup>>
{
    private readonly WebApplicationFactory<ch08.Startup> _factory;

    public BasicFunctionalTests(WebApplicationFactory<ch08.Startup> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Index")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());
    }
}
