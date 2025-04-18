﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.MyDbContext;

#nullable disable

namespace Services.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20250416033152_Migrations")]
    partial class Migrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Proyecto_II.Entities.Cita", b =>
                {
                    b.Property<int>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.Property<int>("TipoCitaId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CitaId");

                    b.HasIndex("SucursalId");

                    b.HasIndex("TipoCitaId");

                    b.HasIndex("UserId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("Proyecto_II.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Nombre = "ADMIN"
                        },
                        new
                        {
                            RoleId = 2,
                            Nombre = "USER"
                        });
                });

            modelBuilder.Entity("Proyecto_II.Entities.Sucursal", b =>
                {
                    b.Property<int>("SucursalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("SucursalId");

                    b.ToTable("Sucursales");

                    b.HasData(
                        new
                        {
                            SucursalId = 1,
                            Nombre = "Clinica Santa Cruz"
                        },
                        new
                        {
                            SucursalId = 2,
                            Nombre = "Clinica Nicoya"
                        },
                        new
                        {
                            SucursalId = 3,
                            Nombre = "Clinica Libera"
                        });
                });

            modelBuilder.Entity("Proyecto_II.Entities.TipoCita", b =>
                {
                    b.Property<int>("TipoCitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TipoCitaId");

                    b.ToTable("TiposCita");

                    b.HasData(
                        new
                        {
                            TipoCitaId = 1,
                            Nombre = "Medicina General"
                        },
                        new
                        {
                            TipoCitaId = 2,
                            Nombre = "Odontología"
                        },
                        new
                        {
                            TipoCitaId = 3,
                            Nombre = "Pediatría"
                        },
                        new
                        {
                            TipoCitaId = 4,
                            Nombre = "Neurología"
                        });
                });

            modelBuilder.Entity("Proyecto_II.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Proyecto_II.Entities.Cita", b =>
                {
                    b.HasOne("Proyecto_II.Entities.Sucursal", "Sucursal")
                        .WithMany("Citas")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_II.Entities.TipoCita", "TipoCita")
                        .WithMany("Citas")
                        .HasForeignKey("TipoCitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proyecto_II.Entities.User", "User")
                        .WithMany("Citas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");

                    b.Navigation("TipoCita");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Proyecto_II.Entities.User", b =>
                {
                    b.HasOne("Proyecto_II.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Proyecto_II.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Proyecto_II.Entities.Sucursal", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Proyecto_II.Entities.TipoCita", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Proyecto_II.Entities.User", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
