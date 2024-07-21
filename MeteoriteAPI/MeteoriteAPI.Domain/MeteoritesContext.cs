using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MeteoriteAPI.Domain.Models.EF;

namespace MeteoriteAPI.Domain
{
    public class MeteoritesContext : DbContext
    {
        public MeteoritesContext(DbContextOptions<MeteoritesContext> options)
            : base(options)
        { }

        public DbSet<Meteorite> Meteorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
