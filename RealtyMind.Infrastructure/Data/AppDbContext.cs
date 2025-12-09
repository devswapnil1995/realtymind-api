using Microsoft.EntityFrameworkCore;
using RealtyMind.Domain.Models;

namespace RealtyMind.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) 
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Property> Properties => Set<Property>();

        public DbSet<Poi> Pois => Set<Poi>();
        public DbSet<NeighborhoodScore> NeighborhoodScores => Set<NeighborhoodScore>();

    }
}
