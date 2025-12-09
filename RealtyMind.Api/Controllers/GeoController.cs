using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.Services.Google;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/geo")]
    public class GeoController : ControllerBase
    {
        private readonly GoogleMapsService _google;

        public GeoController(GoogleMapsService google)
        {
            _google = google;
        }

        [HttpGet("geocode")]
        public async Task<IActionResult> Geocode([FromQuery] string address)
        {
            var response = await _google.GeocodeAsync(address);
            return Ok(response);
        }

        [HttpGet("reverse")]
        public async Task<IActionResult> ReverseGeocode([FromQuery] double lat, [FromQuery] double lng)
        {
            var response = await _google.ReverseGeocodeAsync(lat, lng);
            return Ok(response);
        }
    }
}
