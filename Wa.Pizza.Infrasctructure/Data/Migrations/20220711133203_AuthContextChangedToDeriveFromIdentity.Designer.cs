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
    [Migration("20220711133203_AuthContextChangedToDeriveFromIdentity")]
    partial class AuthContextChangedToDeriveFromIdentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Adress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AdressString")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Adress");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdressString = "Corusan 19",
                            ApplicationUserId = "d45f7c4a-fb87-40e9-9421-68b0a78d2771"
                        },
                        new
                        {
                            Id = 2,
                            AdressString = "Omega-4",
                            ApplicationUserId = "8c1427dc-c42f-4a6c-b524-ee22522c5e7d"
                        },
                        new
                        {
                            Id = 3,
                            AdressString = "Terra-4",
                            ApplicationUserId = "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2"
                        });
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "d45f7c4a-fb87-40e9-9421-68b0a78d2771",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c28f4b35-08e0-44f1-a1ef-65f83aad553b",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "87bc4e8b-dfb9-40a3-b32e-ec46b25a304a",
                            TwoFactorEnabled = false,
                            UserName = "Test"
                        },
                        new
                        {
                            Id = "8c1427dc-c42f-4a6c-b524-ee22522c5e7d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d25ffae8-eaba-4ac5-b76e-7bf2525b6531",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "00fad370-6511-41af-9abf-6fb4a3e1fc6f",
                            TwoFactorEnabled = false,
                            UserName = "Test1"
                        },
                        new
                        {
                            Id = "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d935796c-9849-47b5-b6bb-449ed14e1f81",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "eab048c0-8b27-4b87-aa5a-11e011191e73",
                            TwoFactorEnabled = false,
                            UserName = "Test2"
                        });
                });

            modelBuilder.Entity("Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Basket");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = "d45f7c4a-fb87-40e9-9421-68b0a78d2771",
                            LastModified = new DateTime(2050, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            ApplicationUserId = "8c1427dc-c42f-4a6c-b524-ee22522c5e7d",
                            LastModified = new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            ApplicationUserId = "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2",
                            LastModified = new DateTime(4000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

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

                    b.HasIndex("BasketId");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("BasketItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BasketId = 1,
                            CatalogItemId = 7567,
                            CatalogItemName = "Rex",
                            CatalogType = 3,
                            Quantity = 1,
                            UnitPrice = 10000m
                        },
                        new
                        {
                            Id = 2,
                            BasketId = 1,
                            CatalogItemId = 2224,
                            CatalogItemName = "Cody",
                            CatalogType = 3,
                            Quantity = 1,
                            UnitPrice = 10000m
                        },
                        new
                        {
                            Id = 3,
                            BasketId = 1,
                            CatalogItemId = 1,
                            CatalogItemName = "Clone trooper",
                            CatalogType = 3,
                            Quantity = 3000000,
                            UnitPrice = 4000m
                        },
                        new
                        {
                            Id = 4,
                            BasketId = 2,
                            CatalogItemId = 2,
                            CatalogItemName = "Tomato pizza",
                            CatalogType = 0,
                            Quantity = 500,
                            UnitPrice = 100m
                        },
                        new
                        {
                            Id = 5,
                            BasketId = 3,
                            CatalogItemId = 3,
                            CatalogItemName = "Classic",
                            CatalogType = 0,
                            Quantity = 150,
                            UnitPrice = 150m
                        });
                });

            modelBuilder.Entity("CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

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

                    b.HasData(
                        new
                        {
                            Id = 7567,
                            CatalogType = 3,
                            Description = "Clone trooper commander",
                            Name = "Rex",
                            Price = 10000m,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 2224,
                            CatalogType = 3,
                            Description = "Clone trooper commander",
                            Name = "Cody",
                            Price = 10000m,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 1,
                            CatalogType = 3,
                            Description = "Regular clone trooper",
                            Name = "Clone trooper",
                            Price = 4000m,
                            Quantity = 3000000
                        },
                        new
                        {
                            Id = 2,
                            CatalogType = 0,
                            Description = "With extra Tomato Sauce",
                            Name = "Tomato pizza",
                            Price = 100m,
                            Quantity = 500
                        },
                        new
                        {
                            Id = 3,
                            CatalogType = 0,
                            Description = "Classic",
                            Name = "Pepperoni",
                            Price = 150m,
                            Quantity = 150
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = 66,
                            ApplicationUserId = "d45f7c4a-fb87-40e9-9421-68b0a78d2771",
                            CreationDate = new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The republic will be reogranised into a first galactic empire",
                            Status = 3
                        },
                        new
                        {
                            Id = 1,
                            ApplicationUserId = "8c1427dc-c42f-4a6c-b524-ee22522c5e7d",
                            CreationDate = new DateTime(2186, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Bring extra tomato sauce, don't be late, don't make Aria mad",
                            Status = 2
                        },
                        new
                        {
                            Id = 2,
                            ApplicationUserId = "dbb10eb9-b665-45d7-8f43-fbbdbf1c16a2",
                            CreationDate = new DateTime(4000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Someone order pepperoni pizza into the Emperor's palace",
                            Status = 4
                        });
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<string>("CatalogItemName")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CatalogItemId = 7567,
                            CatalogItemName = "Rex",
                            Discount = 1m,
                            OrderId = 66,
                            Quantity = 1,
                            UnitPrice = 10000m
                        },
                        new
                        {
                            Id = 2,
                            CatalogItemId = 2224,
                            CatalogItemName = "Cody",
                            Discount = 1m,
                            OrderId = 66,
                            Quantity = 1,
                            UnitPrice = 10000m
                        },
                        new
                        {
                            Id = 3,
                            CatalogItemId = 1,
                            CatalogItemName = "Clone trooper",
                            Discount = 0.8m,
                            OrderId = 66,
                            Quantity = 3000000,
                            UnitPrice = 4000m
                        },
                        new
                        {
                            Id = 4,
                            CatalogItemId = 2,
                            CatalogItemName = "Tomato pizza",
                            Discount = 0.01m,
                            OrderId = 1,
                            Quantity = 50,
                            UnitPrice = 100m
                        },
                        new
                        {
                            Id = 5,
                            CatalogItemId = 3,
                            CatalogItemName = "Pepperoni",
                            Discount = 0.99m,
                            OrderId = 2,
                            Quantity = 5,
                            UnitPrice = 150m
                        });
                });

            modelBuilder.Entity("Adress", b =>
                {
                    b.HasOne("ApplicationUser", "ApplicationUser")
                        .WithMany("Adresses")
                        .HasForeignKey("ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.OwnsOne("RefreshToken", "refreshToken", b1 =>
                        {
                            b1.Property<string>("ApplicationUserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("ExpirationDate")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ApplicationUserId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("refreshToken")
                        .IsRequired();
                });

            modelBuilder.Entity("Basket", b =>
                {
                    b.HasOne("ApplicationUser", "ApplicationUser")
                        .WithOne("Basket")
                        .HasForeignKey("Basket", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("BasketItem", b =>
                {
                    b.HasOne("Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatalogItem", "CatalogItem")
                        .WithMany("BasketItems")
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("CatalogItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("ApplicationUser", "ApplicationUser")
                        .WithMany("Orders")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("OrderItem", b =>
                {
                    b.HasOne("CatalogItem", "CatalogItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CatalogItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Basket")
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("CatalogItem", b =>
                {
                    b.Navigation("BasketItems");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}