using Microsoft.Extensions.Caching.Memory;
using RealtyMind.Application.DTOs.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealtyMind.Application.Services.Geo
{
    public class NeighborhoodScoringService
    {
        private readonly OverpassService _overpass;
        private readonly IMemoryCache _cache;
        public NeighborhoodScoringService(OverpassService overpass, IMemoryCache cache)
        {
            _overpass = overpass;
            _cache = cache;
        }

        public async Task<NeighborhoodScoreResult> CalculateScoreAsync(double lat, double lng)
        {
            var cacheKey = $"nb_score_{lat}_{lng}";

            if (_cache.TryGetValue(cacheKey, out NeighborhoodScoreResult cached))
                return cached;

            var pois = await _overpass.GetPoisAsync(lat, lng);

            double transitScore = ComputeCategoryScore(pois, "bus_stop", lat, lng, 1000);
            double schoolScore = ComputeCountScore(pois, "school", 2000, 20);
            double hospitalScore = ComputeCategoryScore(pois, "hospital", lat, lng, 1500);
            double groceryScore = ComputeCategoryScore(pois, "supermarket", lat, lng, 1200);
            double parkScore = ComputeCategoryScore(pois, "park", lat, lng, 1200);

            double crimeScore = 50; // placeholder if no dataset yet

            double final =
                transitScore * 0.25 +
                schoolScore * 0.20 +
                hospitalScore * 0.15 +
                groceryScore * 0.15 +
                parkScore * 0.10 +
                crimeScore * 0.15;

            var breakdown = new
            {
                transitScore,
                schoolScore,
                hospitalScore,
                groceryScore,
                parkScore,
                crimeScore
            };
            

            var result = new NeighborhoodScoreResult
            {
                Score = Math.Round(final, 2),
                BreakdownJson = System.Text.Json.JsonSerializer.Serialize(breakdown),
                CalculatedAt = DateTime.UtcNow
            };
            _cache.Set(cacheKey, result, TimeSpan.FromHours(24));

            return result;
        }

        private double ComputeCountScore(List<PoiDto> pois, string category, int radius, int pointsPerItem)
        {
            var count = pois.Count(x => x.Category == category);
            return Math.Min(100, count * pointsPerItem);
        }

        private double ComputeCategoryScore(List<PoiDto> pois, string category, double lat, double lng, int maxRadius)
        {
            var nearest = pois.Where(x => x.Category == category)
                              .Select(x => Distance(lat, lng, x.Lat, x.Lng))
                              .DefaultIfEmpty(maxRadius)
                              .Min();

            double score = 100 - (nearest / maxRadius * 100);
            return Math.Clamp(score, 0, 100);
        }

        // Haversine
        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371000;
            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLon = (lon2 - lon1) * Math.PI / 180;

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * Math.PI / 180) *
                Math.Cos(lat2 * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            return R * (2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
        }
    }

    public class NeighborhoodScoreResult
    {
        public double Score { get; set; }
        public string BreakdownJson { get; set; } = "{}";
        public DateTime CalculatedAt { get; set; }
    }
}
