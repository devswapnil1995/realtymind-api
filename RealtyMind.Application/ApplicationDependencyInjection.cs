using Microsoft.Extensions.DependencyInjection;
using RealtyMind.Application.Services.Auth;
using RealtyMind.Application.Services.Finance;
using RealtyMind.Application.Services.Geo;
using RealtyMind.Application.Services.Google;
using RealtyMind.Application.Services.Property;

///<summary>
///Register all dependency here
///</summary>
namespace RealtyMind.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<JwtTokenService>();
            services.AddScoped<GoogleMapsService>();
            services.AddScoped<PropertyProviderService>();
            services.AddMemoryCache(); // in Startup/Program, but can be registered here as required
            services.AddScoped<MortgageService>();
            services.AddScoped<LoanCalculatorService>();
            services.AddScoped<OverpassService>();
            services.AddScoped<NeighborhoodScoringService>();

            return services;
        }
    }
}
