using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtyMind.Application.Interfaces;
using RealtyMind.Infrastructure.Data;
using RealtyMind.Infrastructure.Repositories;

namespace RealtyMind.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Register DB Connectext

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMarketPriceIndexRepository, MarketPriceIndexRepository>();

            return services;
        }
    }
}
