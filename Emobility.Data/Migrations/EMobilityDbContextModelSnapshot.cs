﻿// <auto-generated />
using System;
using EMobility.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMobility.Data.Migrations
{
    [DbContext(typeof(EMobilityDbContext))]
    partial class EMobilityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EMobility.Data.ChargingPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChargingPointId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChargingPointId")
                        .IsUnique()
                        .HasFilter("[ChargingPointId] IS NOT NULL");

                    b.ToTable("ChargingPoints");

                    b.HasData(
                        new
                        {
                            Id = -4,
                            ChargingPointId = "CP -4",
                            Name = "",
                            RestUrl = ""
                        },
                        new
                        {
                            Id = -5,
                            ChargingPointId = "CP -5",
                            Name = "",
                            RestUrl = ""
                        },
                        new
                        {
                            Id = 1,
                            ChargingPointId = "CP 1",
                            Name = "",
                            RestUrl = ""
                        },
                        new
                        {
                            Id = 2,
                            ChargingPointId = "CP 2",
                            Name = "",
                            RestUrl = ""
                        },
                        new
                        {
                            Id = 3,
                            ChargingPointId = "CP 3",
                            Name = "",
                            RestUrl = ""
                        },
                        new
                        {
                            Id = 4,
                            ChargingPointId = "CP 4",
                            Name = "",
                            RestUrl = ""
                        });
                });

            modelBuilder.Entity("EMobility.Data.ChargingSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChargingStation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("DurationOf")
                        .HasColumnType("time");

                    b.Property<decimal>("Energy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<TimeSpan>("LocalStartTime")
                        .HasColumnType("time");

                    b.Property<string>("RfidTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ChargingSessions");
                });

            modelBuilder.Entity("EMobility.Data.ConnectionState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChargingPointId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ConnectionStates");
                });

            modelBuilder.Entity("EMobility.Data.RfidTagOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RfidTagId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Until")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RfidTagId")
                        .IsUnique()
                        .HasFilter("[RfidTagId] IS NOT NULL");

                    b.ToTable("RfidTagOwner");

                    b.HasCheckConstraint("UntilAfterFrom", "[Until] IS NULL OR [Until] > [From]");
                });
#pragma warning restore 612, 618
        }
    }
}
