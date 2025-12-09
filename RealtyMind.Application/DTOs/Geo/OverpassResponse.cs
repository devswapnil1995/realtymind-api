using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.DTOs.Geo
{
    public class OverpassResponse
    {
        public List<OverpassElement>? elements { get; set; }
    }
    public class OverpassElement
    {
        public long id { get; set; }
        public string? type { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
        public OverpassCenter? center { get; set; }
        public Dictionary<string, string>? tags { get; set; }
    }
    public class OverpassCenter { public double lat { get; set; } public double lon { get; set; } }
}
