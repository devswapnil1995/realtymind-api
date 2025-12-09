using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RealtyMind.Application.Configurations;
using RealtyMind.Application.DTOs.Geo;
using System.Net.Http.Json;
using System.Text;

namespace RealtyMind.Application.Services.Geo
{
    public class OverpassService
    {
        private readonly HttpClient _http;
        private readonly IMemoryCache _cache;
        private readonly OverpassConfig _config;

        public OverpassService(IHttpClientFactory httpFactory, IOptions<OverpassConfig> config, IMemoryCache cache)
        {
            _http = httpFactory.CreateClient("OverpassClient");
            _cache = cache;
            _config = config.Value;
        }

        public async Task<List<PoiDto>> GetPoisAsync(double lat, double lng, int radius = 1500)
        {
            var cacheKey = $"pois_{lat}_{lng}_{radius}";
            if (_cache.TryGetValue(cacheKey, out List<PoiDto>? cached))
                return cached;

            var query = new StringBuilder();
            query.AppendLine("[out:json][timeout:25];");
            string[] tags = { "school", "hospital", "supermarket", "bus_stop", "train_station", "park" };

            foreach (var tag in tags)
            {
                query.AppendLine($"node[\"amenity\"=\"{tag}\"](around:{radius},{lat},{lng});");
            }

            query.AppendLine("out center;");

            var content = new StringContent(query.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _http.PostAsync("interpreter", content);

            if (!response.IsSuccessStatusCode) return new List<PoiDto>();

            var json = await response.Content.ReadFromJsonAsync<OverpassResponse>();
            var result = new List<PoiDto>();

            if (json?.elements != null)
            {
                foreach (var el in json.elements)
                {
                    double latVal = el.lat ?? el.center?.lat ?? 0;
                    double lngVal = el.lon ?? el.center?.lon ?? 0;

                    result.Add(new PoiDto
                    {
                        ExternalId = $"{el.type}/{el.id}",
                        Provider = "osm",
                        Name = el.tags?.GetValueOrDefault("name") ?? "Unknown",
                        Category = el.tags?.GetValueOrDefault("amenity") ?? "other",
                        Lat = latVal,
                        Lng = lngVal,
                        Address = ""
                    });
                }
            }

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

            return result;
        }
    }
}
