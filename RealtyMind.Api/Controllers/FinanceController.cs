using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.DTOs.Finance;
using RealtyMind.Application.Services.Finance;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/finance")]
    public class FinanceController : ControllerBase
    {
        private readonly MortgageService _mortgageService;
        private readonly LoanCalculatorService _calculator;

        public FinanceController(MortgageService mortgageService, LoanCalculatorService calculator)
        {
            _mortgageService = mortgageService;
            _calculator = calculator;
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetRates([FromQuery] string region = "IN")
        {
            var rate = await _mortgageService.GetCurrentRateAsync(region);
            return Ok(rate);
        }

        [HttpPost("mortgage/calc")]
        public IActionResult CalculateMortgage([FromBody] MortgageCalcRequest req)
        {
            if (req.AnnualRatePercent <= 0)
            {
                // try fetch a default rate if not provided
                var defaultRate = _mortgageService.GetCurrentRateAsync("IN").GetAwaiter().GetResult();
                req.AnnualRatePercent = defaultRate.RateAnnualPercent;
            }

            var resp = _calculator.CalculateMortgage(req);
            return Ok(resp);
        }
    }
}
