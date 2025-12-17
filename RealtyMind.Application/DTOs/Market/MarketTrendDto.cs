using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Market
{    
    public class MarketTrendDto
    {
        public string Locality { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal Growth1YPercent { get; set; }
        public decimal Growth3YPercent { get; set; }
        public string TrendDirection { get; set; } = string.Empty; // Rising / Flat / Falling
    }
}
