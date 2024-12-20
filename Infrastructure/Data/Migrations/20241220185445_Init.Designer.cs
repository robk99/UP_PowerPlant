﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241220185445_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.PowerPlants.PowerPlant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InstallationDate")
                        .HasColumnType("datetime2(3)");

                    b.Property<float>("InstalledPower")
                        .HasColumnType("real");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PowerPlants", t =>
                        {
                            t.HasCheckConstraint("CK_PowerPlant_InstalledPower", "InstalledPower >= 0.1 AND InstalledPower <= 100");

                            t.HasCheckConstraint("CK_PowerPlant_Latitude", "Latitude >= -90 AND Latitude <= 90");

                            t.HasCheckConstraint("CK_PowerPlant_Longitude", "Longitude >= -180 AND Longitude <= 180");
                        });
                });

            modelBuilder.Entity("Domain.PowerProductions.PowerProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PowerPlantId")
                        .HasColumnType("int");

                    b.Property<float>("PowerProduced")
                        .HasColumnType("real");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PowerPlantId", "Timestamp")
                        .IsUnique();

                    b.ToTable("PowerProductions");
                });

            modelBuilder.Entity("Domain.PowerPlants.PowerPlant", b =>
                {
                    b.OwnsOne("Domain.Locations.Location", "Location", b1 =>
                        {
                            b1.Property<int>("PowerPlantId")
                                .HasColumnType("int");

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("DECIMAL(9,6)")
                                .HasColumnName("Latitude");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("DECIMAL(9,6)")
                                .HasColumnName("Longitude");

                            b1.HasKey("PowerPlantId");

                            b1.ToTable("PowerPlants");

                            b1.WithOwner()
                                .HasForeignKey("PowerPlantId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.PowerProductions.PowerProduction", b =>
                {
                    b.HasOne("Domain.PowerPlants.PowerPlant", "PowerPlant")
                        .WithMany("PowerProductions")
                        .HasForeignKey("PowerPlantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PowerPlant");
                });

            modelBuilder.Entity("Domain.PowerPlants.PowerPlant", b =>
                {
                    b.Navigation("PowerProductions");
                });
#pragma warning restore 612, 618
        }
    }
}
