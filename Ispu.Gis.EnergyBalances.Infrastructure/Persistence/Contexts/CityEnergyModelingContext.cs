using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;

/// <summary>
/// Контекст базы данных моделирования городской энергии
/// </summary>
public class CityEnergyModelingContext: DbContext
{
    /// <summary>
    /// Контекст базы данных моделирования городской энергии
    /// </summary>
    /// <param name="options">Опции конфигурирования</param>
    public CityEnergyModelingContext(DbContextOptions<CityEnergyModelingContext> options) : base(options)
    {
    }
    
    /// <summary>
    /// Города
    /// </summary>
    public virtual DbSet<City> Cities { get; set; } = null!;

    /// <summary>
    /// Районы города
    /// </summary>
    public virtual DbSet<CityDistrict> CityDistricts { get; set; } = null!;

    /// <summary>
    /// Здания
    /// </summary>
    public virtual DbSet<Building> Buildings { get; set; } = null!;
    
    /// <summary>
    /// Тепловые трубы
    /// </summary>
    public virtual DbSet<HeatingPipe> HeatingPipes { get; set; } = null!;
    
    /// <summary>
    /// Тепловые станции
    /// </summary>
    public virtual DbSet<HeatingStation> HeatingStations { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("fuzzystrmatch")
            .HasPostgresExtension("tiger", "postgis_tiger_geocoder")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.ApplyConfiguration(new CityMapper());
        modelBuilder.ApplyConfiguration(new CityDistrictMapper());
        modelBuilder.ApplyConfiguration(new BuildingMapper());
        modelBuilder.ApplyConfiguration(new BuildingInfoMapper());
        modelBuilder.ApplyConfiguration(new HeatingPipeMapper());
        modelBuilder.ApplyConfiguration(new HeatingStationMapper());
        
        base.OnModelCreating(modelBuilder);
    }
}