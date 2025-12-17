using RealtyMind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Infrastructure.Data.Seed
{
    public static class MarketIndexSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.MarketPriceIndices.Any()) return;

            var baseDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            for (int i = 0; i < 24; i++)
            {
                db.MarketPriceIndices.Add(new MarketPriceIndex
                {
                    Country = "India",
                    State = "Maharashtra",
                    City = "Mumbai",
                    Locality = "Andheri",
                    Period = baseDate.AddMonths(i),
                    AvgPricePerSqFt = 18000 + (i * 150), // rising trend
                    Source = "manual"
                });
            }

            db.SaveChanges();
        }
    }
}
