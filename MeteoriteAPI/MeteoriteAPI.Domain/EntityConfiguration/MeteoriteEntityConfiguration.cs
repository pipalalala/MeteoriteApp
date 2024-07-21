using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeteoriteAPI.Domain.Models.EF;

namespace MeteoriteAPI.Domain.EntityConfiguration
{
    public class MeteoriteEntityConfiguration : IEntityTypeConfiguration<Meteorite>
    {
        public void Configure(EntityTypeBuilder<Meteorite> builder)
        {
            builder
                .ToTable("meteorites")
                .HasKey(e => e.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("id");

            builder
                .Property(p => p.MeteoriteId)
                .HasColumnName("meteorite_id")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired();

            builder
                .Property(p => p.NameType)
                .HasColumnName("nametype")
                .IsRequired();

            builder
                .Property(p => p.RecClass)
                .HasColumnName("recclass")
                .IsRequired();

            builder
               .Property(p => p.Mass)
               .HasColumnName("mass")
               .HasPrecision(18, 6)
               .IsRequired(false);

            builder
               .Property(p => p.Fall)
               .HasColumnName("fall")
               .IsRequired();

            builder
               .Property(p => p.Year)
               .HasColumnName("year")
               .IsRequired(false);

            builder
               .Property(p => p.RecLat)
               .HasColumnName("reclat")
               .HasPrecision(18, 6)
               .IsRequired(false);

            builder
               .Property(p => p.RecLong)
               .HasColumnName("reclong")
               .HasPrecision(18, 6)
               .IsRequired(false);

            builder
               .Property(p => p.GeolocationType)
               .HasColumnName("type")
               .IsRequired(false);

            builder
               .Property(p => p.GeolocationCoordinatesLong)
               .HasColumnName("coordinates_long")
               .HasPrecision(18, 5)
               .IsRequired(false);

            builder
               .Property(p => p.GeolocationCoordinatesLat)
               .HasColumnName("coordinates_lat")
               .HasPrecision(18, 5)
               .IsRequired(false);


            builder
                .HasIndex(e => e.RecClass);

            builder
                .HasIndex(e => e.Name);
        }
    }
}
