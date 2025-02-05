﻿// <auto-generated />
using System;
using FleetControl.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FleetControl.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(FleetControlDbContext))]
    partial class FleetControlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FleetControl.Core.Entities.CostCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("CostCenters");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.DriverProjects", b =>
                {
                    b.Property<int>("IdDriver")
                        .HasColumnType("int");

                    b.Property<int>("IdProject")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("IdDriver", "IdProject");

                    b.HasIndex("IdProject");

                    b.ToTable("DriverProjects");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("IdCostCenter")
                        .HasColumnType("int");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdCostCenter");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("IdDriver")
                        .HasColumnType("int");

                    b.Property<int>("IdVehicle")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdDriver");

                    b.HasIndex("IdVehicle");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.ReservationComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<int>("IdReservation")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdReservation");

                    b.HasIndex("IdUser");

                    b.ToTable("ReservationComments");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("IdProject")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastMaintenance")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<int>("MileAge")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdProject");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.VehicleMaintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdVehicle")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdVehicle");

                    b.ToTable("VehicleMaintenance");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Driver", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.DriverProjects", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.Driver", "Driver")
                        .WithMany("DriverProjects")
                        .HasForeignKey("IdDriver")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FleetControl.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Project", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.CostCenter", "CostCenter")
                        .WithMany()
                        .HasForeignKey("IdCostCenter")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FleetControl.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CostCenter");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Reservation", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FleetControl.Core.Entities.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("IdDriver")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FleetControl.Core.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("IdVehicle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Driver");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.ReservationComment", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.Reservation", "Reservation")
                        .WithMany("ReservationComments")
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FleetControl.Core.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Vehicle", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("IdProject");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.VehicleMaintenance", b =>
                {
                    b.HasOne("FleetControl.Core.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("IdVehicle")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Driver", b =>
                {
                    b.Navigation("DriverProjects");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.Reservation", b =>
                {
                    b.Navigation("ReservationComments");
                });

            modelBuilder.Entity("FleetControl.Core.Entities.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
