using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Domain.Models
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
