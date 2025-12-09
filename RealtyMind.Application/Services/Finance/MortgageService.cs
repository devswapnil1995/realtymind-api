using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RealtyMind.Application.Configurations;
using RealtyMind.Application.DTOs.Finance;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace RealtyMind.Application.Services.Finance
{
    public class MortgageService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly RapidApiConfig _rapidConfig; // reuse RapidApi config if needed
        private readonly MortgageConfig _mortgageConfig;
        private readonly IMemoryCache _cache;
        private const string RatesCacheKey = "mortgage_rates_v1";

        public MortgageService(IHttpClientFactory httpFactory,
                               IOptions<RapidApiConfig> rapidConfig,
                               IOptions<MortgageConfig> mortgageConfig,
                               IMemoryCache cache)
        {
            _httpFactory = httpFactory;
            _rapidConfig = rapidConfig.Value;
            _mortgageConfig = mortgageConfig.Value;
            _cache = cache;
        }

        // Returns a simple rate object. If external provider is configured, try to fetch it.
        public async Task<MortgageRateDto> GetCurrentRateAsync(string region = "IN")
        {
            // Try cache
            if (_cache.TryGetValue<RentalRateWrapper>(RatesCacheKey + "_" + region, out var cachedWrapper))
            {
                return cachedWrapper.Rate;
            }

            MortgageRateDto rate;

            if (!string.IsNullOrWhiteSpace(_mortgageConfig.ApiKey) && _mortgageConfig.Provider != "None")
            {
                try
                {
                    // Example: if provider is 'SomeProvider', we call using IHttpClientFactory
                    // For now, attempt a simple RapidAPI endpoint if configured; else fallback.
                    var client = _httpFactory.CreateClient("RapidApiClient");

                    // NOTE: adapt the real endpoint/path for the mortgage-rate provider you choose.
                    // This is a placeholder example:
                    var resp = await client.GetFromJsonAsync<Dictionary<string, object>>($"some-mortgage-endpoint?region={region}");
                    // Parse response to decimal rate if provider exists.

                    decimal parsedRate = 7.5m; // fallback placeholder
                    rate = new MortgageRateDto
                    {
                        Provider = _mortgageConfig.Provider ?? "external",
                        Region = region,
                        RateAnnualPercent = parsedRate,
                        Points = 0m,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch
                {
                    // fallback to default below
                    rate = DefaultRate(region);
                }
            }
            else
            {
                rate = DefaultRate(region);
            }

            // Cache short-term (10 minutes)
            _cache.Set(RatesCacheKey + "_" + region, new RentalRateWrapper { Rate = rate }, TimeSpan.FromMinutes(10));

            return rate;
        }

        private MortgageRateDto DefaultRate(string region)
        {
            // sensible defaults by region - adjust for production
            var fallback = region.ToUpper() switch
            {
                "US" => 6.5m,
                "UK" => 4.0m,
                "AU" => 5.25m,
                "IN" => 8.5m,
                _ => 7.0m
            };

            return new MortgageRateDto
            {
                Provider = "LocalFallback",
                Region = region,
                RateAnnualPercent = fallback,
                Points = 0m,
                TimestampUtc = DateTime.UtcNow
            };
        }

        private class RentalRateWrapper { public MortgageRateDto Rate { get; set; } = new(); }
    }
}
