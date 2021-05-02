﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceParkAPI.Data;

namespace SpaceparkAPI.Migrations
{
    [DbContext(typeof(SpaceParkContext))]
    [Migration("20210502084307_AddedSpacePortId")]
    partial class AddedSpacePortId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpaceparkAPI.Models.Parking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SpacePortId")
                        .HasColumnType("int");

                    b.Property<string>("StarShip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<decimal?>("TotalSum")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Traveller")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SpacePortId");

                    b.ToTable("Parkings");
                });

            modelBuilder.Entity("SpaceparkAPI.Models.SpacePort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SpacePorts");
                });

            modelBuilder.Entity("SpaceparkAPI.Models.Parking", b =>
                {
                    b.HasOne("SpaceparkAPI.Models.SpacePort", null)
                        .WithMany("Parkings")
                        .HasForeignKey("SpacePortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpaceparkAPI.Models.SpacePort", b =>
                {
                    b.Navigation("Parkings");
                });
#pragma warning restore 612, 618
        }
    }
}
