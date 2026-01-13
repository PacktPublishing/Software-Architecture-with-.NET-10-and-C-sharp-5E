using System.Collections.Generic;

namespace ch08.Services
{
    public interface ITravelDistanceService
    {
        double CalculateDistance(string city1, string city2);
        IEnumerable<string> GetAvailableCities();
    }
}
