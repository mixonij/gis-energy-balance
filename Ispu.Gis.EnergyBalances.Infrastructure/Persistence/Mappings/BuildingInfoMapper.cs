using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер информации о здании
/// </summary>
public class BuildingInfoMapper: IEntityTypeConfiguration<BuildingInfo>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<BuildingInfo> builder)
    {
        // Первичный ключ
        builder.HasKey(e => e.Id).HasName("building_information_pkey");

        // Таблица
        builder.ToTable("building_information");

        // Индекс
        builder.HasIndex(e => e.Id, "IX_building_information_id");

        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0)
            .IsRequired();
        
        // Идентификатор здания
        builder.Property(e => e.BuildingId)
            .HasColumnName("building_id")
            .HasColumnOrder(1);
        
        // Идентификатор района
        builder.Property(e => e.BuiltYear)
            .HasColumnName("built_year")
            .HasColumnOrder(2);
        
        // Геометрия
        builder.Property(e => e.ResidentsCount)
            .HasColumnName("residents_count")
            .HasColumnOrder(3);
        
        // Геометрия
        builder.Property(e => e.Area)
            .HasColumnName("residential_area")
            .HasColumnOrder(4);
        
        // Геометрия
        builder.Property(e => e.RegistryId)
            .HasColumnName("registry_id")
            .HasColumnOrder(5);
    }
}