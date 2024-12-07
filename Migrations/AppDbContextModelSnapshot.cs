﻿// <auto-generated />
using System;
using HexAsset.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HexAsset.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HexAsset.Models.Asset", b =>
                {
                    b.Property<int>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetId"));

                    b.Property<string>("AssetCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssetValue")
                        .HasColumnType("int");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssetId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("HexAsset.Models.AssetAllocation", b =>
                {
                    b.Property<int>("AllocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AllocationId"));

                    b.Property<DateTime>("AllocationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AllocationStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AllocationId");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("AssetAllocations");
                });

            modelBuilder.Entity("HexAsset.Models.AssetRequest", b =>
                {
                    b.Property<int>("AssetRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetRequestId"));

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AssetRequestId");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("AssetRequests");
                });

            modelBuilder.Entity("HexAsset.Models.AuditRequest", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuditId"));

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AuditStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AuditId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditRequests");
                });

            modelBuilder.Entity("HexAsset.Models.ServiceRequest", b =>
                {
                    b.Property<int>("ServiceRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceRequestId"));

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ServiceRequestId");

                    b.HasIndex("AssetId");

                    b.HasIndex("UserId");

                    b.ToTable("ServiceRequests");
                });

            modelBuilder.Entity("HexAsset.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HexAsset.Models.AssetAllocation", b =>
                {
                    b.HasOne("HexAsset.Models.Asset", "Asset")
                        .WithMany("AssetAllocations")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HexAsset.Models.User", "User")
                        .WithMany("AssetAllocations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HexAsset.Models.AssetRequest", b =>
                {
                    b.HasOne("HexAsset.Models.Asset", "Asset")
                        .WithMany("AssetRequests")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HexAsset.Models.User", "User")
                        .WithMany("AssetRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HexAsset.Models.AuditRequest", b =>
                {
                    b.HasOne("HexAsset.Models.User", "User")
                        .WithMany("AuditRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HexAsset.Models.ServiceRequest", b =>
                {
                    b.HasOne("HexAsset.Models.Asset", "Asset")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HexAsset.Models.User", "User")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HexAsset.Models.Asset", b =>
                {
                    b.Navigation("AssetAllocations");

                    b.Navigation("AssetRequests");

                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("HexAsset.Models.User", b =>
                {
                    b.Navigation("AssetAllocations");

                    b.Navigation("AssetRequests");

                    b.Navigation("AuditRequests");

                    b.Navigation("ServiceRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
