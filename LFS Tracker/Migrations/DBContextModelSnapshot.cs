﻿// <auto-generated />
using LFS_Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LFS_Tracker.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LFS_Tracker.Models.LfsInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("InstanceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("LfsVersion")
                        .HasColumnType("real");

                    b.Property<float>("Version")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("LfsInstance");
                });

            modelBuilder.Entity("LFS_Tracker.Models.Package", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<bool>("IsCoreLfsPackage")
                        .HasColumnType("bit");

                    b.Property<float>("LfsVersion")
                        .HasColumnType("real");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Version")
                        .HasColumnType("real");

                    b.HasKey("PackageId");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("LfsInstancePackage", b =>
                {
                    b.Property<int>("InstalledInstancesId")
                        .HasColumnType("int");

                    b.Property<int>("InstalledPackagesPackageId")
                        .HasColumnType("int");

                    b.HasKey("InstalledInstancesId", "InstalledPackagesPackageId");

                    b.HasIndex("InstalledPackagesPackageId");

                    b.ToTable("LfsInstancePackage");
                });

            modelBuilder.Entity("LfsInstancePackage", b =>
                {
                    b.HasOne("LFS_Tracker.Models.LfsInstance", null)
                        .WithMany()
                        .HasForeignKey("InstalledInstancesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LFS_Tracker.Models.Package", null)
                        .WithMany()
                        .HasForeignKey("InstalledPackagesPackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
