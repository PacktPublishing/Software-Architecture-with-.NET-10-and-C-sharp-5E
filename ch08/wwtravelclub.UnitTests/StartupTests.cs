using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace wwtravelclub.UnitTests;

public class StartupTests
{
    [Fact]
    public void Startup_ConfigureServices_RegistersServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        var startup = new ch08.Startup(configuration);

        // Act
        startup.ConfigureServices(services);

        // Assert
        // Verify that services were registered by ConfigureServices
        Assert.NotEmpty(services);
        Assert.True(services.Count > 0, "ConfigureServices should register at least one service");
    }
}
