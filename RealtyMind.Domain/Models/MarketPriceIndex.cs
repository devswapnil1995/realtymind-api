using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Domain.Models
{    
    public class MarketPriceIndex
    {
        public Guid Id { get; set; }

        // Region identifiers
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Locality { get; set; } = string.Empty;

        // Time series
        public DateTime Period { get; set; }   // month start
        public decimal AvgPricePerSqFt { get; set; }

        // Metadata
        public string Source { get; set; } = "manual";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
