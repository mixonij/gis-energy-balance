using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер тепловых труб
/// </summary>
public class HeatingPipeMapper: IEntityTypeConfiguration<HeatingPipe>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<HeatingPipe> builder)
    {
        // Первичный ключ
        builder.HasKey(e => e.Id).HasName("heating_pipes_pkey");

        // Таблица
        builder.ToTable("heating_pipes");

        // Индекс
        builder.HasIndex(e => e.Id, "IX_heating_pipes_id");

        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0)
            .IsRequired();
        
        // Диаметр подающей трубы
        builder.Property(e => e.DPod)
            .HasColumnName("d_pod")
            .HasColumnOrder(1);
        
        // Диаметр обратки
        builder.Property(e => e.DObr)
            .HasColumnName("d_obr")
            .HasColumnOrder(2);
        
        // Идентификатор тепловой станции
        builder.Property(e => e.HeatingStationId)
            .HasColumnName("heating_station_id")
            .HasColumnOrder(3);
        
        // Геометрия
        builder.Property(e => e.Geometry)
            .HasColumnType("geometry(MultiLineString,4326)")
            .HasColumnName("geometry")
            .HasColumnOrder(4)
            .IsRequired();
    }
}