﻿// <auto-generated />
using System;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Ispu.Gis.EnergyBalances.Infrastructure.Migrations
{
    [DbContext(typeof(EnergyBalancesContext))]
    partial class EnergyBalancesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<NpgsqlPoint[]>("PolygonCoordinates")
                        .IsRequired()
                        .HasColumnType("point[]")
                        .HasColumnName("polygon_coordinates");

                    b.HasKey("Id")
                        .HasName("areas_pkey");

                    b.ToTable("areas", (string)null);
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<NpgsqlPoint>("Coordinates")
                        .HasColumnType("point")
                        .HasColumnName("coordinates");

                    b.Property<NpgsqlPoint[]>("PolygonCoordinates")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("point[]")
                        .HasColumnName("polygon_coordinates")
                        .HasDefaultValueSql("ARRAY[]::point[]");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex(new[] { "CityId" }, "IX_buildings_city_id");

                    b.ToTable("buildings", (string)null);
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.BuildingsInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer")
                        .HasColumnName("building_id");

                    b.Property<int?>("BuiltYear")
                        .HasColumnType("integer")
                        .HasColumnName("built_year");

                    b.HasKey("Id")
                        .HasName("buildings_info_pkey");

                    b.HasIndex("BuildingId");

                    b.ToTable("buildings_info", (string)null);
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("MinZoom")
                        .HasColumnType("integer")
                        .HasColumnName("min_zoom");

                    b.Property<string>("NameRussian")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("name_russian")
                        .HasDefaultValueSql("''::text");

                    b.Property<NpgsqlPoint>("NorthWestBound")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("point")
                        .HasColumnName("north_west_bound")
                        .HasDefaultValueSql("'(0,0)'::point");

                    b.Property<NpgsqlPoint>("SouthEastBound")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("point")
                        .HasColumnName("south_east_bound")
                        .HasDefaultValueSql("'(0,0)'::point");

                    b.HasKey("Id")
                        .HasName("cities_pkey");

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Building", b =>
                {
                    b.HasOne("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.City", "City")
                        .WithMany("Buildings")
                        .HasForeignKey("CityId")
                        .IsRequired()
                        .HasConstraintName("city_id");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.BuildingsInfo", b =>
                {
                    b.HasOne("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Building", "Building")
                        .WithMany("BuildingsInfos")
                        .HasForeignKey("BuildingId")
                        .IsRequired()
                        .HasConstraintName("building_id");

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Building", b =>
                {
                    b.Navigation("BuildingsInfos");
                });

            modelBuilder.Entity("Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.City", b =>
                {
                    b.Navigation("Buildings");
                });
#pragma warning restore 612, 618
        }
    }
}
