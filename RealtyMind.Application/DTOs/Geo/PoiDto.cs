using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Geo
{
    public class PoiDto
    {
        public string ExternalId { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
