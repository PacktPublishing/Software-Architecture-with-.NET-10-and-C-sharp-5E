using Microsoft.Extensions.Logging;
using Moq;
using ch08.Pages;
using ch08.Services;
using System.Collections.Generic;

namespace wwtravelclub.UnitTests;

public class TravelDistanceModelTests
{
    [Fact]
    public void OnGet_InitializesAvailableCities()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<TravelDistanceModel>>();
        var mockService = new Mock<ITravelDistanceService>();
        var expectedCities = new List<string> { "New York", "London", "Paris" };
        mockService.Setup(s => s.GetAvailableCities()).Returns(expectedCities);

        var model = new TravelDistanceModel(mockLogger.Object, mockService.Object);

        // Act
        model.OnGet();

        // Assert
        Assert.NotNull(model.AvailableCities);
        Assert.Equal(expectedCities, model.AvailableCities);
    }

    [Fact]
    public void OnPost_WithValidCities_CalculatesDistance()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<TravelDistanceModel>>();
        var mockService = new Mock<ITravelDistanceService>();
        var expectedCities = new List<string> { "New York", "London" };
        mockService.Setup(s => s.GetAvailableCities()).Returns(expectedCities);
        mockService.Setup(s => s.CalculateDistance("New York", "London")).Returns(5571.0);

        var model = new TravelDistanceModel(mockLogger.Object, mockService.Object)
        {
            City1 = "New York",
            City2 = "London"
        };

        // Act
        var result = model.OnPost();

        // Assert
        Assert.NotNull(model.Distance);
        Assert.Equal(5571.0, model.Distance.Value);
        Assert.Null(model.ErrorMessage);
    }

    [Fact]
    public void OnPost_WithEmptyCities_SetsErrorMessage()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<TravelDistanceModel>>();
        var mockService = new Mock<ITravelDistanceService>();
        var expectedCities = new List<string> { "New York", "London" };
        mockService.Setup(s => s.GetAvailableCities()).Returns(expectedCities);

        var model = new TravelDistanceModel(mockLogger.Object, mockService.Object)
        {
            City1 = "",
            City2 = ""
        };

        // Act
        var result = model.OnPost();

        // Assert
        Assert.Null(model.Distance);
        Assert.NotNull(model.ErrorMessage);
        Assert.Contains("select both cities", model.ErrorMessage);
    }

    [Fact]
    public void OnPost_WithInvalidCity_SetsErrorMessage()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<TravelDistanceModel>>();
        var mockService = new Mock<ITravelDistanceService>();
        var expectedCities = new List<string> { "New York", "London" };
        mockService.Setup(s => s.GetAvailableCities()).Returns(expectedCities);
        mockService.Setup(s => s.CalculateDistance("New York", "InvalidCity"))
            .Throws(new System.ArgumentException("City 'InvalidCity' not found"));

        var model = new TravelDistanceModel(mockLogger.Object, mockService.Object)
        {
            City1 = "New York",
            City2 = "InvalidCity"
        };

        // Act
        var result = model.OnPost();

        // Assert
        Assert.Null(model.Distance);
        Assert.NotNull(model.ErrorMessage);
        Assert.Contains("not found", model.ErrorMessage);
    }
}
