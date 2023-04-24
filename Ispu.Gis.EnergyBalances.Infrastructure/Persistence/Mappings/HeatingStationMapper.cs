using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер тепловой станции
/// </summary>
public class HeatingStationMapper: IEntityTypeConfiguration<HeatingStation>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<HeatingStation> builder)
    {
        // Первичный ключ
        builder.HasKey(e => e.Id).HasName("heating_stations_pkey");

        // Таблица
        builder.ToTable("heating_stations");

        // Индекс
        builder.HasIndex(e => e.Id, "IX_heating_stations_id");

        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0)
            .IsRequired();
        
        // Номинальная мощность
        builder.Property(e => e.NominalPower)
            .HasColumnName("nominal_power")
            .HasColumnOrder(1);
        
        // Идентификатор города
        builder.Property(e => e.CityId)
            .HasColumnName("city_id")
            .HasColumnOrder(2);
        
        // Идентификатор города
        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasColumnOrder(3);
        
        // Геометрия
        builder.Property(e => e.Geometry)
            .HasColumnType("geometry(Polygon,4326)")
            .HasColumnName("geometry")
            .HasColumnOrder(4)
            .IsRequired();
        
        // Отношение один ко многим для тепловых труб
        builder.HasMany(e => e.HeatingPipes)
            .WithOne(e => e.HeatingStation)
            .HasForeignKey(e => e.HeatingStationId)
            .HasPrincipalKey(e => e.Id);
    }
}