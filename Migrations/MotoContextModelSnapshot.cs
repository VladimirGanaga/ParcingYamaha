﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParcingYamaha;

#nullable disable

namespace ParcingYamaha.Migrations
{
    [DbContext(typeof(MotoContext))]
    partial class MotoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParcingYamaha.ClassesDB.ChaptersDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ModelsDBID")
                        .HasColumnType("int");

                    b.Property<string>("chapter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chapterID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("partFile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelsDBID");

                    b.ToTable("ChapterDB");
                });

            modelBuilder.Entity("ParcingYamaha.ClassesDB.ModelsDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("colorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("colorType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelTypeCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("modelYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prodPictureFileURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ModelDB");
                });

            modelBuilder.Entity("ParcingYamaha.ClassesDB.PartsDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("chapterDBId")
                        .HasColumnType("int");

                    b.Property<int>("chapterID")
                        .HasColumnType("int");

                    b.Property<string>("partName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("partNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("refNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("chapterDBId");

                    b.ToTable("PartDB");
                });

            modelBuilder.Entity("ParcingYamaha.ClassesDB.ChaptersDB", b =>
                {
                    b.HasOne("ParcingYamaha.ClassesDB.ModelsDB", "modelsDB")
                        .WithMany()
                        .HasForeignKey("ModelsDBID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("modelsDB");
                });

            modelBuilder.Entity("ParcingYamaha.ClassesDB.PartsDB", b =>
                {
                    b.HasOne("ParcingYamaha.ClassesDB.ChaptersDB", "chapterDB")
                        .WithMany()
                        .HasForeignKey("chapterDBId");

                    b.Navigation("chapterDB");
                });
#pragma warning restore 612, 618
        }
    }
}
