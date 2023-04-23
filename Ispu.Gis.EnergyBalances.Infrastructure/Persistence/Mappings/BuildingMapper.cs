using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер зданий
/// </summary>
public class BuildingMapper: IEntityTypeConfiguration<Building>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        // Первичный ключ
        builder.HasKey(e => e.Id).HasName("buildings_pkey");

        // Таблица
        builder.ToTable("buildings");

        // Индекс
        builder.HasIndex(e => e.Id, "IX_buildings_id");

        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0)
            .IsRequired();
        
        // Идентификатор города
        builder.Property(e => e.CityId)
            .HasColumnName("city_id")
            .HasColumnOrder(1);
        
        // Идентификатор района
        builder.Property(e => e.DistrictId)
            .HasColumnName("district_id")
            .HasColumnOrder(2);
        
        // Геометрия
        builder.Property(e => e.Geometry)
            .HasColumnType("geometry(Polygon,4326)")
            .HasColumnName("geometry")
            .HasColumnOrder(3)
            .IsRequired();
    }
}