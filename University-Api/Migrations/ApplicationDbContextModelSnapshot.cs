﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityApi.Data;

#nullable disable

namespace UniversityApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UniversityApi.Model.Deans", b =>
                {
                    b.Property<int>("DeanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeanId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeanId");

                    b.ToTable("Deans");
                });

            modelBuilder.Entity("UniversityApi.Model.Faculties", b =>
                {
                    b.Property<int>("FacultieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultieId"), 1L, 1);

                    b.Property<int>("DeansId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniversitysId")
                        .HasColumnType("int");

                    b.HasKey("FacultieId");

                    b.HasIndex("DeansId")
                        .IsUnique();

                    b.HasIndex("UniversitysId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("UniversityApi.Model.Groups", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<int>("FacultiesId")
                        .HasColumnType("int");

                    b.Property<int>("NumberGroup")
                        .HasColumnType("int");

                    b.Property<int>("ProfessionId")
                        .HasColumnType("int");

                    b.Property<int>("ProfessionsProfessionId")
                        .HasColumnType("int");

                    b.Property<int>("YearCome")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("FacultiesId");

                    b.HasIndex("ProfessionsProfessionId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UniversityApi.Model.Managers", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("UniversityApi.Model.Professions", b =>
                {
                    b.Property<int>("ProfessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfessionId"), 1L, 1);

                    b.Property<int>("FacultieId")
                        .HasColumnType("int");

                    b.Property<int?>("FacultiesFacultieId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfessionId");

                    b.HasIndex("FacultiesFacultieId");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("UniversityApi.Model.Students", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("GroupsGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupsGroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityApi.Model.Universitys", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UniversityId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagersId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniversityId");

                    b.HasIndex("ManagersId")
                        .IsUnique();

                    b.ToTable("Universitys");
                });

            modelBuilder.Entity("UniversityApi.Model.Faculties", b =>
                {
                    b.HasOne("UniversityApi.Model.Deans", "Deans")
                        .WithOne("Faculties")
                        .HasForeignKey("UniversityApi.Model.Faculties", "DeansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityApi.Model.Universitys", "Universitys")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversitysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deans");

                    b.Navigation("Universitys");
                });

            modelBuilder.Entity("UniversityApi.Model.Groups", b =>
                {
                    b.HasOne("UniversityApi.Model.Faculties", "Faculties")
                        .WithMany("Groups")
                        .HasForeignKey("FacultiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityApi.Model.Professions", "Professions")
                        .WithMany("Groups")
                        .HasForeignKey("ProfessionsProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculties");

                    b.Navigation("Professions");
                });

            modelBuilder.Entity("UniversityApi.Model.Professions", b =>
                {
                    b.HasOne("UniversityApi.Model.Faculties", "Faculties")
                        .WithMany("Professions")
                        .HasForeignKey("FacultiesFacultieId");

                    b.Navigation("Faculties");
                });

            modelBuilder.Entity("UniversityApi.Model.Students", b =>
                {
                    b.HasOne("UniversityApi.Model.Groups", "Groups")
                        .WithMany("Students")
                        .HasForeignKey("GroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniversityApi.Model.Universitys", b =>
                {
                    b.HasOne("UniversityApi.Model.Managers", "Managers")
                        .WithOne("Universitys")
                        .HasForeignKey("UniversityApi.Model.Universitys", "ManagersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Managers");
                });

            modelBuilder.Entity("UniversityApi.Model.Deans", b =>
                {
                    b.Navigation("Faculties")
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityApi.Model.Faculties", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Professions");
                });

            modelBuilder.Entity("UniversityApi.Model.Groups", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("UniversityApi.Model.Managers", b =>
                {
                    b.Navigation("Universitys")
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityApi.Model.Professions", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniversityApi.Model.Universitys", b =>
                {
                    b.Navigation("Faculties");
                });
#pragma warning restore 612, 618
        }
    }
}
