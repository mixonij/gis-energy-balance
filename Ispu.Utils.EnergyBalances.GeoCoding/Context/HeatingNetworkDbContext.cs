using System;
using System.Collections.Generic;
using Ispu.Utils.EnergyBalances.GeoCoding.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Context;

public partial class HeatingNetworkDbContext : DbContext
{
    public HeatingNetworkDbContext()
    {
    }

    public HeatingNetworkDbContext(DbContextOptions<HeatingNetworkDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Heatingnetworkiv> Heatingnetworkivs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=pipes", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");

        modelBuilder.Entity<Heatingnetworkiv>(entity =>
        {
            entity.HasKey(e => e.OgcFid).HasName("heatingnetworkiv_pk");

            entity.ToTable("heatingnetworkiv");

            entity.HasIndex(e => e.WkbGeometry, "heatingnetworkiv_wkb_geometry_geom_idx").HasMethod("gist");

            entity.Property(e => e.OgcFid).HasColumnName("ogc_fid");
            entity.Property(e => e.Dobr)
                .HasPrecision(19, 11)
                .HasColumnName("dobr");
            entity.Property(e => e.Dpod)
                .HasPrecision(19, 11)
                .HasColumnName("dpod");
            entity.Property(e => e.Objectid)
                .HasPrecision(10)
                .HasColumnName("objectid");
            entity.Property(e => e.ShapeLeng)
                .HasPrecision(19, 11)
                .HasColumnName("shape_leng");
            entity.Property(e => e.Sys)
                .HasPrecision(19, 11)
                .HasColumnName("sys");
            entity.Property(e => e.WkbGeometry)
                .HasColumnType("geometry(MultiLineString,3857)")
                .HasColumnName("wkb_geometry");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
