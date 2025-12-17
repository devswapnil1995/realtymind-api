using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RealtyMind.Application.Configurations;
using RealtyMind.Application.DTOs.Google;
using System.Net.Http.Json;

namespace RealtyMind.Application.Services.Google
{
    public class GoogleMapsService
    {
        private readonly HttpClient _http;
        private readonly GoogleConfig _config;
        private readonly IMemoryCache _cache;
        public GoogleMapsService(IHttpClientFactory httpClientFactory, IOptions<GoogleConfig> config, IMemoryCache cache)
        {
            _http = httpClientFactory.CreateClient("GoogleMaps");
            _config = config.Value;
            _cache = cache;
        }

        public async Task<GeocodeResponse?> GeocodeAsync(string address)
        {
            var cacheKey = $"geo_{address.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out GeocodeResponse cached))
                return cached;

            var url = $"geocode/json?address={Uri.EscapeDataString(address)}&key={_config.ApiKey}";
            var response = await _http.GetFromJsonAsync<GeocodeResponse>(url);

            _cache.Set(cacheKey, response, TimeSpan.FromHours(6));
            return response;

        }

        public async Task<GeocodeResponse?> ReverseGeocodeAsync(double lat, double lng)
        {
            var url = $"geocode/json?latlng={lat},{lng}&key={_config.ApiKey}";
            return await _http.GetFromJsonAsync<GeocodeResponse>(url);
        }
    }
}
