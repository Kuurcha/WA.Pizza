﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Wa.Pizza.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220310211546_RelationsFix1")]
    partial class RelationsFix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Adress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdressString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Adress");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("adressID")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("adressID");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Basket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BasketItem")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("basketItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("basketItemId");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("BasketItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CatalogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CatalogItemName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("CatalogType")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId")
                        .IsUnique();

                    b.ToTable("BasketItem");
                });

            modelBuilder.Entity("CatalogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CatalogType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CatalogItem");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("OrderItemId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CatalogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CatalogItemName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId")
                        .IsUnique();

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.HasOne("Adress", "adress")
                        .WithMany()
                        .HasForeignKey("adressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("adress");
                });

            modelBuilder.Entity("Basket", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithOne("basket")
                        .HasForeignKey("Basket", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BasketItem", "basketItem")
                        .WithMany()
                        .HasForeignKey("basketItemId");

                    b.Navigation("basketItem");
                });

            modelBuilder.Entity("BasketItem", b =>
                {
                    b.HasOne("CatalogItem", "catalogItem")
                        .WithOne("basketItem")
                        .HasForeignKey("BasketItem", "CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("catalogItem");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("ApplicationUser", "applicationUser")
                        .WithMany("Orders")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("OrderItem", "orderItem")
                        .WithMany("Orders")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("applicationUser");

                    b.Navigation("orderItem");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.HasOne("CatalogItem", "catalogItem")
                        .WithOne("orderItem")
                        .HasForeignKey("OrderItem", "CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("catalogItem");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("basket")
                        .IsRequired();
                });

            modelBuilder.Entity("CatalogItem", b =>
                {
                    b.Navigation("basketItem")
                        .IsRequired();

                    b.Navigation("orderItem")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}