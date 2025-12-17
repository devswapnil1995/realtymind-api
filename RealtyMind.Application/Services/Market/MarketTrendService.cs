using Microsoft.Extensions.Caching.Memory;
using RealtyMind.Application.DTOs.Market;
using RealtyMind.Application.Interfaces;
using System.Threading;

namespace RealtyMind.Application.Services.Market
{
    public class MarketTrendService
    {
        private readonly IMarketPriceIndexRepository _repo;
        private readonly IMemoryCache _cache;

        public MarketTrendService(IMarketPriceIndexRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<MarketTrendDto?> GetTrendAsync(
            string city,
            string locality)
        {
            var cacheKey = $"trend_{city}_{locality}";

            if (_cache.TryGetValue(cacheKey, out MarketTrendDto cached))
                return cached;

            var data = await _repo.GetByCityAndLocalityAsync(city, locality);

            if (data.Count < 2) return null;

            var latest = data.Last();

            var oneYearAgo = data.LastOrDefault(x =>
                x.Period <= latest.Period.AddYears(-1));

            var threeYearAgo = data.LastOrDefault(x =>
                x.Period <= latest.Period.AddYears(-3));

            decimal growth1Y = oneYearAgo == null
                ? 0
                : (latest.AvgPricePerSqFt - oneYearAgo.AvgPricePerSqFt)
                    / oneYearAgo.AvgPricePerSqFt * 100;

            decimal growth3Y = threeYearAgo == null
                ? 0
                : (latest.AvgPricePerSqFt - threeYearAgo.AvgPricePerSqFt)
                    / threeYearAgo.AvgPricePerSqFt * 100;

            string direction =
                growth1Y > 5 ? "Rising" :
                growth1Y < -5 ? "Falling" :
                "Stable";

            var trend = new MarketTrendDto
            {
                Locality = locality,
                CurrentPrice = latest.AvgPricePerSqFt,
                Growth1YPercent = Math.Round(growth1Y, 2),
                Growth3YPercent = Math.Round(growth3Y, 2),
                TrendDirection = direction
            };

            _cache.Set(cacheKey, trend, TimeSpan.FromHours(24));
            return trend;

        }
    }
}
