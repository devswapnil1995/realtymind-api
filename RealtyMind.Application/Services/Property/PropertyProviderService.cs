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

        public PropertyProviderService(IHttpClientFactory httpClientFactory, IOptions<RapidApiConfig> config)
        {
            _http = httpClientFactory.CreateClient("RapidApiClient");
            _config = config.Value;

            _http.DefaultRequestHeaders.Add("X-RapidAPI-Key", _config.Key);
            _http.DefaultRequestHeaders.Add("X-RapidAPI-Host", _config.ZillowHost);
        }

        public async Task<ZillowPropertyResponse?> GetPropertyDetailsAsync(string address)
        {
            var url = $"search?address={Uri.EscapeDataString(address)}";

            return await _http.GetFromJsonAsync<ZillowPropertyResponse>(url);
        }
    }
}
