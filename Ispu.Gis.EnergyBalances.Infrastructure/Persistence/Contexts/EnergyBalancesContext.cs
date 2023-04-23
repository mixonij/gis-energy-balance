using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;

public partial class EnergyBalancesContext : DbContext
{
    public EnergyBalancesContext()
    {
    }

//     public EnergyBalancesContext(DbContextOptions<EnergyBalancesContext> options)
//         : base(options)
//     {
//     }
//
//     public virtual DbSet<CityDistrict> Areas { get; set; }
//
//     public virtual DbSet<Building> Buildings { get; set; }
//
//     public virtual DbSet<BuildingsInfo> BuildingsInfos { get; set; }
//
//     public virtual DbSet<City> Cities { get; set; }
//
//     public virtual DbSet<HeatingStation> HeatingStations { get; set; }
//
//     public virtual DbSet<Heatingnetworkiv> Heatingnetworkivs { get; set; }
//
//     public virtual DbSet<House> Houses { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=energy_balances;UserId=postgres;Password=postgres;", x => x.UseNetTopologySuite());
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder
//             .HasPostgresExtension("fuzzystrmatch")
//             .HasPostgresExtension("postgis")
//             .HasPostgresExtension("tiger", "postgis_tiger_geocoder")
//             .HasPostgresExtension("topology", "postgis_topology");
//
//         modelBuilder.Entity<CityDistrict>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("areas_pkey");
//
//             entity.ToTable("areas");
//
//             entity.Property(e => e.Id)
//                 .UseIdentityAlwaysColumn()
//                 .HasColumnName("id");
//             entity.Property(e => e.CityId).HasColumnName("city_id");
//             //entity.Property(e => e.PolygonCoordinates).HasColumnName("polygon_coordinates");
//         });
//
//         modelBuilder.Entity<Building>(entity =>
//         {
//             // entity.HasKey(e => e.Id).HasName("id");
//             //
//             // entity.ToTable("buildings");
//             //
//             // entity.HasIndex(e => e.CityId, "IX_buildings_city_id");
//             //
//             // entity.Property(e => e.Id)
//             //     .UseIdentityAlwaysColumn()
//             //     .HasColumnName("id");
//             // entity.Property(e => e.CityId).HasColumnName("city_id");
//             // entity.Property(e => e.Coordinates).HasColumnName("coordinates");
//             // entity.Property(e => e.PolygonCoordinates)
//             //     .HasDefaultValueSql("ARRAY[]::point[]")
//             //     .HasColumnName("polygon_coordinates");
//             //
//             // entity.HasOne(d => d.City).WithMany(p => p.Buildings)
//             //     .HasForeignKey(d => d.CityId)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("city_id");
//         });
//
//         modelBuilder.Entity<BuildingsInfo>(entity =>
//         {
//             // entity.HasKey(e => e.Id).HasName("buildings_info_pkey");
//             //
//             // entity.ToTable("buildings_info");
//             //
//             // entity.HasIndex(e => e.BuildingId, "IX_buildings_info_building_id");
//             //
//             // entity.Property(e => e.Id)
//             //     .UseIdentityAlwaysColumn()
//             //     .HasColumnName("id");
//             // entity.Property(e => e.Area).HasColumnName("area");
//             // entity.Property(e => e.BuildingId).HasColumnName("building_id");
//             // entity.Property(e => e.BuiltYear).HasColumnName("built_year");
//             // entity.Property(e => e.ResidentsCount).HasColumnName("residents_count");
//             //
//             // entity.HasOne(d => d.Building).WithMany(p => p.BuildingsInfos)
//             //     .HasForeignKey(d => d.BuildingId)
//             //     .OnDelete(DeleteBehavior.ClientSetNull)
//             //     .HasConstraintName("building_id");
//         });
//
//         modelBuilder.Entity<City>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("cities_pkey");
//
//             entity.ToTable("cities");
//
//             entity.Property(e => e.Id)
//                 .UseIdentityAlwaysColumn()
//                 .HasColumnName("id");
//             entity.Property(e => e.MinZoom).HasColumnName("min_zoom");
//             entity.Property(e => e.NativeName)
//                 .HasDefaultValueSql("''::text")
//                 .HasColumnName("name_russian");
//             entity.Property(e => e.NorthWestPoint)
//                 .HasDefaultValueSql("'(0,0)'::point")
//                 .HasColumnName("north_west_bound");
//             entity.Property(e => e.SouthEastPoint)
//                 .HasDefaultValueSql("'(0,0)'::point")
//                 .HasColumnName("south_east_bound");
//         });
//
//         modelBuilder.Entity<HeatingStation>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("heating_stations_pkey");
//
//             entity.ToTable("heating_stations");
//
//             entity.HasIndex(e => e.CityId, "IX_heating_stations_city_id");
//
//             entity.Property(e => e.Id)
//                 .UseIdentityAlwaysColumn()
//                 .HasColumnName("id");
//             entity.Property(e => e.CityId).HasColumnName("city_id");
//             entity.Property(e => e.Coords).HasColumnName("coords");
//             entity.Property(e => e.NominalPower)
//                 .HasDefaultValueSql("1")
//                 .HasColumnName("nominal_power");
//
//             entity.HasOne(d => d.City).WithMany(p => p.HeatingStations)
//                 .HasForeignKey(d => d.CityId)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("city_id");
//         });
//
//         modelBuilder.Entity<Heatingnetworkiv>(entity =>
//         {
//             entity.HasKey(e => e.OgcFid).HasName("heatingnetworkiv_pk");
//
//             entity.ToTable("heatingnetworkiv");
//
//             entity.HasIndex(e => e.WkbGeometry, "heatingnetworkiv_wkb_geometry_geom_idx").HasMethod("gist");
//
//             entity.Property(e => e.OgcFid).HasColumnName("ogc_fid");
//             entity.Property(e => e.Dobr)
//                 .HasPrecision(19, 11)
//                 .HasColumnName("dobr");
//             entity.Property(e => e.Dpod)
//                 .HasPrecision(19, 11)
//                 .HasColumnName("dpod");
//             entity.Property(e => e.Objectid)
//                 .HasPrecision(10)
//                 .HasColumnName("objectid");
//             entity.Property(e => e.ShapeLeng)
//                 .HasPrecision(19, 11)
//                 .HasColumnName("shape_leng");
//             entity.Property(e => e.Sys)
//                 .HasPrecision(19, 11)
//                 .HasColumnName("sys");
//             entity.Property(e => e.WkbGeometry)
//                 .HasColumnType("geometry(MultiLineString,3857)")
//                 .HasColumnName("wkb_geometry");
//         });
//
//         modelBuilder.Entity<House>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("houses_pkey");
//
//             entity.ToTable("houses");
//
//             entity.Property(e => e.Id)
//                 .ValueGeneratedNever()
//                 .HasColumnName("id");
//             entity.Property(e => e.Address).HasColumnName("address");
//             entity.Property(e => e.Coords).HasColumnName("coords");
//             entity.Property(e => e.Description)
//                 .HasColumnType("jsonb")
//                 .HasColumnName("description");
//         });
//
//         OnModelCreatingPartial(modelBuilder);
//     }
//
//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
