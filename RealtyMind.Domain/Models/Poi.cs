using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Domain.Models
{
    public class Poi
    {
        public Guid Id { get; set; }
        public string Provider { get; set; } = string.Empty;   // "osm" | "google" | "local"
        public string ExternalId { get; set; } = string.Empty; // provider id
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;   // e.g., "school","hospital","transit","supermarket"
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
    }
}
