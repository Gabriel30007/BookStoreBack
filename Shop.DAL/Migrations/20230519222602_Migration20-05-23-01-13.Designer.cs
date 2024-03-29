﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.DAL.AppDbContexts;

#nullable disable

namespace Shop.DAL.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20230519222602_Migration20-05-23-01-13")]
    partial class Migration2005230113
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop.DAL.Entities.Bucket", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("productID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("userID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("productID");

                    b.HasIndex("userID");

                    b.ToTable("Bucket");
                });

            modelBuilder.Entity("Shop.DAL.Entities.Product", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Shop.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Shop.DAL.Entities.Bucket", b =>
                {
                    b.HasOne("Shop.DAL.Entities.Product", null)
                        .WithMany("Buckets")
                        .HasForeignKey("productID");

                    b.HasOne("Shop.DAL.Entities.User", null)
                        .WithMany("Buckets")
                        .HasForeignKey("userID");
                });

            modelBuilder.Entity("Shop.DAL.Entities.Product", b =>
                {
                    b.Navigation("Buckets");
                });

            modelBuilder.Entity("Shop.DAL.Entities.User", b =>
                {
                    b.Navigation("Buckets");
                });
#pragma warning restore 612, 618
        }
    }
}
