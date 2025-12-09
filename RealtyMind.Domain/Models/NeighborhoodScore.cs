using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Domain.Models
{
    public class NeighborhoodScore
    {
        public Guid Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Score { get; set; }          // 0..100
        public string BreakdownJson { get; set; } = "{}"; // JSON with component scores
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
    }
}
