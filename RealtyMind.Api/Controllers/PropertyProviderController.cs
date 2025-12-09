using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.Services.Property;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/property/provider")]
    public class PropertyProviderController : ControllerBase
    {
        private readonly PropertyProviderService _service;

        public PropertyProviderController(PropertyProviderService service)
        {
            _service = service;
        }

        [HttpGet("zillow")]
        public async Task<IActionResult> GetPropertyDetails([FromQuery] string address)
        {
            var response = await _service.GetPropertyDetailsAsync(address);
            return Ok(response);
        }
    }
}
