using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.Services.Market;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/market")]
    public class MarketController : ControllerBase
    {
        private readonly MarketTrendService _service;

        public MarketController(MarketTrendService service)
        {
            _service = service;
        }

        [HttpGet("trend")]
        public async Task<IActionResult> GetTrend(
            string city,
            string locality)
        {
            var trend = await _service.GetTrendAsync(city, locality);
            if (trend == null) return NotFound();

            return Ok(trend);
        }
    }
}
