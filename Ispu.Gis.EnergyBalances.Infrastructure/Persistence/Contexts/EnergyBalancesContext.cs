using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;

/// <summary>
/// Контекст базы данных энергетических балансов
/// </summary>
public partial class EnergyBalancesContext : DbContext
{
    /// <summary>
    /// Контекст базы данных энергетических балансов
    /// </summary>
    public EnergyBalancesContext()
    {
    }

    /// <summary>
    /// Контекст базы данных энергетических балансов
    /// </summary>
    /// <param name="options">Опции контекста бд</param>
    public EnergyBalancesContext(DbContextOptions<EnergyBalancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; } = null!;

    public virtual DbSet<City> Cities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id");

            entity.ToTable("buildings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Coordinates).HasColumnName("coordinates");

            entity.HasOne(d => d.City).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_id");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NameRussian).HasColumnName("name_russian");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
