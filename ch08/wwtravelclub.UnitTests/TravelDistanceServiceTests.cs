using System;
using System.Linq;
using ch08.Services;

namespace wwtravelclub.UnitTests;

public class TravelDistanceServiceTests
{
    [Fact]
    public void GetAvailableCities_ReturnsNonEmptyList()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act
        var cities = service.GetAvailableCities();

        // Assert
        Assert.NotNull(cities);
        Assert.NotEmpty(cities);
    }

    [Fact]
    public void GetAvailableCities_ReturnsOrderedList()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act
        var cities = service.GetAvailableCities().ToList();

        // Assert
        Assert.Equal(cities.OrderBy(c => c).ToList(), cities);
    }

    [Fact]
    public void CalculateDistance_ValidCities_ReturnsPositiveDistance()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act
        var distance = service.CalculateDistance("New York", "Los Angeles");

        // Assert
        Assert.True(distance > 0);
        Assert.InRange(distance, 3900, 4000); // Approximate distance in km
    }

    [Fact]
    public void CalculateDistance_SameCity_ReturnsZero()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act
        var distance = service.CalculateDistance("New York", "New York");

        // Assert
        Assert.Equal(0, distance, 1); // Allow small rounding error
    }

    [Fact]
    public void CalculateDistance_NullCity1_ThrowsArgumentException()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => service.CalculateDistance(null, "Los Angeles"));
    }

    [Fact]
    public void CalculateDistance_EmptyCity2_ThrowsArgumentException()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => service.CalculateDistance("New York", ""));
    }

    [Fact]
    public void CalculateDistance_InvalidCity_ThrowsArgumentException()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => 
            service.CalculateDistance("New York", "InvalidCity"));
        Assert.Contains("not found", exception.Message);
    }

    [Fact]
    public void CalculateDistance_KnownCities_ReturnsExpectedRange()
    {
        // Arrange
        var service = new TravelDistanceService();

        // Act
        var nyToLondon = service.CalculateDistance("New York", "London");
        var londonToParis = service.CalculateDistance("London", "Paris");

        // Assert
        Assert.InRange(nyToLondon, 5500, 5600); // Approximate distance in km
        Assert.InRange(londonToParis, 300, 400); // Approximate distance in km
    }
}
