using System;

namespace RealtyMind.Application.DTOs.Geo
{
    public class NeighborhoodScoreResult
    {
        public double Score { get; set; }
        public string BreakdownJson { get; set; } = string.Empty;
        public DateTime CalculatedAt { get; set; }
    }
}
