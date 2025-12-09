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

        public GoogleMapsService(IHttpClientFactory httpClientFactory, IOptions<GoogleConfig> config)
        {
            _http = httpClientFactory.CreateClient("GoogleMaps");
            _config = config.Value;
        }

        public async Task<GeocodeResponse?> GeocodeAsync(string address)
        {
            var url = $"geocode/json?address={Uri.EscapeDataString(address)}&key={_config.ApiKey}";
            return await _http.GetFromJsonAsync<GeocodeResponse>(url);
        }

        public async Task<GeocodeResponse?> ReverseGeocodeAsync(double lat, double lng)
        {
            var url = $"geocode/json?latlng={lat},{lng}&key={_config.ApiKey}";
            return await _http.GetFromJsonAsync<GeocodeResponse>(url);
        }
    }
}
