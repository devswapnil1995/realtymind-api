using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealtyMind.Application.DTOs.Geo;
using RealtyMind.Application.Services.Geo;
using RealtyMind.Domain.Models;
using RealtyMind.Infrastructure.Data;

namespace RealtyMind.Api.Controllers
{
    [ApiController]
    [Route("api/neighborhood")]
    public class NeighborhoodController : ControllerBase
    {
        private readonly OverpassService _overpass;
        private readonly NeighborhoodScoringService _scoring;
        private readonly AppDbContext _db;

        public NeighborhoodController(
            OverpassService overpass,
            NeighborhoodScoringService scoring,
            AppDbContext db)
        {
            _overpass = overpass;
            _scoring = scoring;
            _db = db;
        }

        [HttpGet("pois")]
        public async Task<IActionResult> Pois(double lat, double lng, int radius = 1500)
        {
            var result = await _overpass.GetPoisAsync(lat, lng, radius);
            return Ok(result);
        }

        [HttpGet("score")]
        public async Task<IActionResult> Score(double lat, double lng)
        {
            var result = await _scoring.CalculateScoreAsync(lat, lng);

            _db.NeighborhoodScores.Add(new NeighborhoodScore
            {
                Id = Guid.NewGuid(),
                Lat = lat,
                Lng = lng,
                Score = result.Score,
                BreakdownJson = result.BreakdownJson,
                CalculatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();

            return Ok(result);
        }
    }
}
