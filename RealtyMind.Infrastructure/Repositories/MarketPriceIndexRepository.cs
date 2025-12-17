using Microsoft.EntityFrameworkCore;
using RealtyMind.Application.Interfaces;
using RealtyMind.Domain.Models;
using RealtyMind.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Infrastructure.Repositories
{
    public class MarketPriceIndexRepository : IMarketPriceIndexRepository
    {
        private readonly AppDbContext _db;

        public MarketPriceIndexRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<MarketPriceIndex>> GetByCityAndLocalityAsync(
            string city,
            string locality)
        {
            return await _db.MarketPriceIndices
                .Where(x => x.City == city && x.Locality == locality)
                .OrderBy(x => x.Period)
                .ToListAsync();
        }
    }
}
