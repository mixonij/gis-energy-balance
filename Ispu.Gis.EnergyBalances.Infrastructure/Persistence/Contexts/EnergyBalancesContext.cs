using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;

public class EnergyBalancesContext : DbContext
{
    public EnergyBalancesContext()
    {
    }

    public EnergyBalancesContext(DbContextOptions<EnergyBalancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<BuildingsInfo> BuildingsInfos { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=energy_balances;UserId=postgres;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("areas_pkey");
            entity.ToTable("areas");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.PolygonCoordinates).HasColumnName("polygon_coordinates");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id");

            entity.ToTable("buildings");

            entity.HasIndex(e => e.CityId, "IX_buildings_city_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Coordinates).HasColumnName("coordinates");
            entity.Property(e => e.PolygonCoordinates)
                .HasDefaultValueSql("ARRAY[]::point[]")
                .HasColumnName("polygon_coordinates");

            entity.HasOne(d => d.City).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_id");
        });

        modelBuilder.Entity<BuildingsInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buildings_info_pkey");

            entity.ToTable("buildings_info");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BuildingId).HasColumnName("building_id");
            entity.Property(e => e.BuiltYear).HasColumnName("built_year");

            entity.HasOne(d => d.Building).WithMany(p => p.BuildingsInfos)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("building_id");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.MinZoom).HasColumnName("min_zoom");
            entity.Property(e => e.NameRussian)
                .HasDefaultValueSql("''::text")
                .HasColumnName("name_russian");
            entity.Property(e => e.NorthWestBound)
                .HasDefaultValueSql("'(0,0)'::point")
                .HasColumnName("north_west_bound");
            entity.Property(e => e.SouthEastBound)
                .HasDefaultValueSql("'(0,0)'::point")
                .HasColumnName("south_east_bound");
        });
    }
}
