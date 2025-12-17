using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RealtyMind.Application.Configurations;
using RealtyMind.Application.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace RealtyMind.Application.Services.Property
{
    public class PropertyProviderService
    {
        private readonly HttpClient _http;
        private readonly RapidApiConfig _config;
        private readonly IMemoryCache _cache;

        public PropertyProviderService(IHttpClientFactory httpClientFactory, IOptions<RapidApiConfig> config, IMemoryCache cache)
        {
            _http = httpClientFactory.CreateClient("RapidApiClient");
            _config = config.Value;
            _cache = cache;

            _http.DefaultRequestHeaders.Add("X-RapidAPI-Key", _config.Key);
            _http.DefaultRequestHeaders.Add("X-RapidAPI-Host", _config.ZillowHost);
        }

        public async Task<ZillowPropertyResponse?> GetPropertyDetailsAsync(string address)
        {
            var cacheKey = $"zillow_{address.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out ZillowPropertyResponse cached))
                return cached;

            var url = $"search?address={Uri.EscapeDataString(address)}";
            var response = await _http.GetFromJsonAsync<ZillowPropertyResponse>(url);

            _cache.Set(cacheKey, response, TimeSpan.FromHours(12));
            return response;
        }
    }
}
