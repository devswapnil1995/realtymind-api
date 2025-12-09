using Microsoft.Extensions.DependencyInjection;
using RealtyMind.Application.Services.Auth;

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
            return services;
        }
    }
}
