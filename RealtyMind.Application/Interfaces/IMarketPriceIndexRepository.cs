using RealtyMind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyMind.Application.Interfaces
{
    public interface IMarketPriceIndexRepository
    {
        Task<List<MarketPriceIndex>> GetByCityAndLocalityAsync(
            string city,
            string locality);
    }
}
