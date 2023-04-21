using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;

/// <summary>
/// Маппер сущности город
/// </summary>
public class CityMapper: IEntityTypeConfiguration<City>
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="builder">Конструктор сущностей</param>
    public void Configure(EntityTypeBuilder<City> builder)
    {
        // Первичный ключ
        builder.HasKey(x => x.Id).HasName("cities_pkey");
        
        // Таблица
        builder.ToTable("cities");
        
        // Идентификатор
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn()
            .HasColumnName("id")
            .HasColumnOrder(0);
        
        // минимальный зум области
        builder.Property(e => e.MinZoom)
            .HasColumnName("min_zoom")
            .HasColumnOrder(1)
            .IsRequired();
        
        // Название
        builder.Property(e => e.Name)
            .HasDefaultValueSql("''::text")
            .HasColumnName("name")
            .HasColumnOrder(2)
            .IsRequired();
        
        // Название на русском
        builder.Property(e => e.NativeName)
            .HasDefaultValueSql("''::text")
            .HasColumnName("name_native")
            .HasColumnOrder(3)
            .IsRequired();
        
        // Северо-западная точка области просмотра
        builder.Property(e => e.NorthWestPoint)
            .HasDefaultValueSql("'(0,0)'::point")
            .HasColumnName("north_west_point")
            .HasColumnOrder(4)
            .IsRequired();
        
        // Юго-восточная точка области просмотра
        builder.Property(e => e.SouthEastPoint)
            .HasDefaultValueSql("'(0,0)'::point")
            .HasColumnName("south_east_point")
            .HasColumnOrder(5)
            .IsRequired();

        // Отношение один ко многим для городских районов
        builder.HasMany(e => e.CityDistricts)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId)
            .HasPrincipalKey(e => e.Id);

        // Дефолтная строка с городом
        builder.HasData(new City
        {
            Id = 1,
            MinZoom = 11,
            Name = "Ivanovo",
            NativeName = "Иваново",
            NorthWestPoint = new NpgsqlPoint(57.093009, 40.661774),
            SouthEastPoint = new NpgsqlPoint(56.903771, 41.30722)
        });
    }
}