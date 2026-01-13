using System;
using System.Collections.Generic;
using System.Linq;

namespace ch08.Services
{
    public class TravelDistanceService : ITravelDistanceService
    {
        private readonly Dictionary<string, (double Latitude, double Longitude)> _cities;

        public TravelDistanceService()
        {
            // Initialize with some common cities and their coordinates
            _cities = new Dictionary<string, (double, double)>
            {
                { "New York", (40.7128, -74.0060) },
                { "Los Angeles", (34.0522, -118.2437) },
                { "Chicago", (41.8781, -87.6298) },
                { "Houston", (29.7604, -95.3698) },
                { "Phoenix", (33.4484, -112.0740) },
                { "Philadelphia", (39.9526, -75.1652) },
                { "San Antonio", (29.4241, -98.4936) },
                { "San Diego", (32.7157, -117.1611) },
                { "Dallas", (32.7767, -96.7970) },
                { "San Jose", (37.3382, -121.8863) },
                { "London", (51.5074, -0.1278) },
                { "Paris", (48.8566, 2.3522) },
                { "Tokyo", (35.6762, 139.6503) },
                { "Sydney", (-33.8688, 151.2093) },
                { "Dubai", (25.2048, 55.2708) }
            };
        }

        public double CalculateDistance(string city1, string city2)
        {
            if (string.IsNullOrWhiteSpace(city1) || string.IsNullOrWhiteSpace(city2))
            {
                throw new ArgumentException("City names cannot be null or empty");
            }

            if (!_cities.ContainsKey(city1))
            {
                throw new ArgumentException($"City '{city1}' not found");
            }

            if (!_cities.ContainsKey(city2))
            {
                throw new ArgumentException($"City '{city2}' not found");
            }

            var (lat1, lon1) = _cities[city1];
            var (lat2, lon2) = _cities[city2];

            return CalculateHaversineDistance(lat1, lon1, lat2, lon2);
        }

        public IEnumerable<string> GetAvailableCities()
        {
            return _cities.Keys.OrderBy(c => c);
        }

        private double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusKm = 6371.0;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
