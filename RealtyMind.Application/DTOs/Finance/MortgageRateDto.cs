using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Finance
{
    public class MortgageRateDto
    {
        public string Provider { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public decimal RateAnnualPercent { get; set; }    // e.g. 7.25 (percent)
        public decimal Points { get; set; }               // e.g. 0.0
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
