using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер городских районов
/// </summary>
public class CityDistrictMapper : IEntityTypeConfiguration<CityDistrict>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<CityDistrict> builder)
    {
        // Первичный ключ
        builder.HasKey(e => e.Id).HasName("areas_pkey");

        // Таблица
        builder.ToTable("city_districts");

        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0)
            .IsRequired();
        
        // Идентификатор города
        builder.Property(e => e.CityId)
            .HasColumnName("city_id")
            .HasColumnOrder(1)
            .IsRequired();

        // Геометрия
        builder.Property(e => e.Geometry)
            .HasColumnType("geometry(Polygon,4326)")
            .HasColumnName("geometry")
            .HasColumnOrder(2)
            .IsRequired();
        
        // Отношение один ко многим для зданий района
        builder.HasMany(e => e.Buildings)
            .WithOne(e => e.CityDistrict)
            .HasForeignKey(e => e.DistrictId)
            .HasPrincipalKey(e => e.Id);
    }
}