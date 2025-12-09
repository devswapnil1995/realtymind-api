using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Property
{
    public class ZillowPropertyResponse
    {
        public string Status { get; set; } = string.Empty;
        public ZillowPropertyData Data { get; set; } = new();
    }

    public class ZillowPropertyData
    {
        public string? Address { get; set; }
        public double? Price { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public double? LivingArea { get; set; }
        public double? LotSize { get; set; }
        public string? HomeType { get; set; }
        public string? Zestimate { get; set; }
        public string? RentZestimate { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
