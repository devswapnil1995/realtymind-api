using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace RealtyMind.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // build config
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // will be Infrastructure project folder when executed with --project
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var cs = configuration.GetConnectionString("DefaultConnection")
                     ?? "Host=localhost;Port=5432;Database=realitymind;Username=rm_user;Password=Ronaldo@1401";

            builder.UseNpgsql(cs, o => o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            return new AppDbContext(builder.Options);
        }
    }
}