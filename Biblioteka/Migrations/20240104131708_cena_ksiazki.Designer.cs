﻿// <auto-generated />
using System;
using Biblioteka.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biblioteka.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240104131708_cena_ksiazki")]
    partial class cena_ksiazki
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("Author")
                        .HasColumnType("int");

                    b.Property<int>("Book")
                        .HasColumnType("int");

                    b.HasKey("Author", "Book");

                    b.HasIndex("Book");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("Biblioteka.Models.Author", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("authorPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Authors", (string)null);
                });

            modelBuilder.Entity("Biblioteka.Models.Book", b =>
                {
                    b.Property<int>("bookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookId"));

                    b.Property<int?>("Book")
                        .HasColumnType("int");

                    b.Property<long>("ISBN")
                        .HasColumnType("bigint");

                    b.Property<string>("bookPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<DateTime>("releaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("stockLevel")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("bookId");

                    b.HasIndex("Book");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Biblioteka.Models.Rental", b =>
                {
                    b.Property<int>("rentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("rentalId"));

                    b.Property<string>("rentalCity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("rentalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("rentalState")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("rentalStreet")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("stateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("rentalId");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("Biblioteka.Models.RentalBook", b =>
                {
                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("rentalId")
                        .HasColumnType("int");

                    b.HasIndex("bookId");

                    b.HasIndex("rentalId");

                    b.ToTable("RentalBook");
                });

            modelBuilder.Entity("Biblioteka.Models.Tag", b =>
                {
                    b.Property<int>("tagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tagId"));

                    b.Property<string>("tagName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("tagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Biblioteka.Models.TagBook", b =>
                {
                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<int>("tagId")
                        .HasColumnType("int");

                    b.HasIndex("bookId");

                    b.HasIndex("tagId");

                    b.ToTable("TagBook");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("Biblioteka.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("Author")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteka.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("Book")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Biblioteka.Models.Book", b =>
                {
                    b.HasOne("Biblioteka.Models.Rental", null)
                        .WithMany("book")
                        .HasForeignKey("Book");
                });

            modelBuilder.Entity("Biblioteka.Models.RentalBook", b =>
                {
                    b.HasOne("Biblioteka.Models.Book", "book")
                        .WithMany()
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteka.Models.Rental", "rental")
                        .WithMany()
                        .HasForeignKey("rentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("rental");
                });

            modelBuilder.Entity("Biblioteka.Models.TagBook", b =>
                {
                    b.HasOne("Biblioteka.Models.Book", "book")
                        .WithMany()
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteka.Models.Tag", "tag")
                        .WithMany()
                        .HasForeignKey("tagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("tag");
                });

            modelBuilder.Entity("Biblioteka.Models.Rental", b =>
                {
                    b.Navigation("book");
                });
#pragma warning restore 612, 618
        }
    }
}
