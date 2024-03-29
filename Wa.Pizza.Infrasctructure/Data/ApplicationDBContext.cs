﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wa.Pizza.Core.Model.AuthenticateController;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Adress> Adress { get; set; }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    public DbSet<Basket> Basket { get; set; }

    public DbSet<Order> ShopOrder { get; set; }

    public DbSet<BasketItem> BasketItem { get; set; }

    public DbSet<OrderItem> ShopOrderItem { get; set; }

    public DbSet<CatalogItem> CatalogItem { get; set; }

    public DbSet<RefreshToken> RefreshToken { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    private void seedDate(ModelBuilder modelBuilder)
    {
        Adress[] adressess = new Adress[]
          {
                new Adress { Id = 1, AdressString = "Corusan 19" },
                new Adress { Id = 2, AdressString = "Omega-4"},
                new Adress { Id = 3, AdressString = "Terra-4"}
          };

        Order[] orders = new Order[]
            {
                new Order { Id = 66, CreationDate = new DateTime(2019, 12, 31), Description = "The republic will be reogranised into a first galactic empire", Status = OrderStatus.Delivered },
                new Order { Id = 1, CreationDate = new DateTime(2186, 10, 11), Description = "Bring extra tomato sauce, don't be late, don't make Aria mad", Status = OrderStatus.Delivering},
                new Order { Id = 2, CreationDate = new DateTime(4000, 1, 12), Description = "Someone order pepperoni pizza into the Emperor's palace", Status = OrderStatus.Canceled}

            };
        CatalogItem[] catalogItems = new CatalogItem[]
         {
            new CatalogItem {  Id = 7567,  Quantity = 1, Name = "Rex", Description = "Clone trooper commander", Price = 10000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets },
            new CatalogItem {  Id = 2224,  Quantity = 1, Name = "Cody", Description = "Clone trooper commander", Price = 10000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets },
            new CatalogItem {  Id = 1,  Quantity = 3000000, Name = "Clone trooper", Description = "Regular clone trooper", Price = 4000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets},
            new CatalogItem {  Id = 2,  Quantity = 500, Name = "Tomato pizza", Description = "With extra Tomato Sauce", Price = 100, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Pizza },
            new CatalogItem {  Id = 3,  Quantity = 150, Name = "Pepperoni", Description = "Classic", Price = 150, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Pizza },

         };
        OrderItem[] orderItems = new OrderItem[]
        {
            new OrderItem { Id = 1, Discount = 1, Quantity = 1, UnitPrice = 10000, OrderId = orders[0].Id, CatalogItemName = catalogItems[0].Name, CatalogItemId = catalogItems[0].Id  },
            new OrderItem { Id = 2, Discount = 1, Quantity = 1, UnitPrice = 10000, OrderId = orders[0].Id, CatalogItemName = catalogItems[1].Name, CatalogItemId = catalogItems[1].Id},
            new OrderItem { Id = 3, Discount = 0.8M, Quantity = 3000000, UnitPrice = 4000, OrderId = orders[0].Id, CatalogItemName = catalogItems[2].Name, CatalogItemId = catalogItems[2].Id },
            new OrderItem { Id = 4, Discount = 0.01M, Quantity = 50, UnitPrice = 100,      OrderId = orders[1].Id, CatalogItemName = catalogItems[3].Name, CatalogItemId = catalogItems[3].Id },
            new OrderItem { Id = 5, Discount = 0.99M, Quantity = 5, UnitPrice = 150,       OrderId = orders[2].Id, CatalogItemName = catalogItems[4].Name, CatalogItemId = catalogItems[4].Id },

        };





        BasketItem[] basketItems = new BasketItem[]
        {
            new BasketItem { Id = 1, Quantity = 1, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 10000, CatalogItemName = "Rex",  CatalogItemId = catalogItems[0].Id,  },
            new BasketItem { Id = 2, Quantity = 1, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 10000, CatalogItemName = "Cody", CatalogItemId = catalogItems[1].Id },
            new BasketItem { Id = 3, Quantity = 3000000, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 4000, CatalogItemName = "Clone trooper", CatalogItemId = catalogItems[2].Id },
            new BasketItem { Id = 4, Quantity = 500, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza, UnitPrice = 100, CatalogItemName = "Tomato pizza", CatalogItemId = catalogItems[3].Id },
            new BasketItem { Id = 5, Quantity = 150, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItemId = catalogItems[4].Id },
        };



        Basket[] baskets = new Basket[]
        {
            new Basket { Id = 1, LastModified = new DateTime(2050,12,10) },
            new Basket { Id = 2, LastModified = new DateTime(2186, 10, 11) },
            new Basket { Id = 3, LastModified = new DateTime(4000, 1, 13) },
        };

        basketItems[0].BasketId = baskets[0].Id;
        basketItems[1].BasketId = baskets[0].Id;
        basketItems[2].BasketId = baskets[0].Id;
        basketItems[3].BasketId = baskets[1].Id;
        basketItems[4].BasketId = baskets[2].Id;



        ApplicationUser[] applicationUsers = new ApplicationUser[]
      {
            new ApplicationUser { UserName = "Test" },
            new ApplicationUser { UserName = "Test1" },
            new ApplicationUser { UserName = "Test2" }
      };

        for (int i = 0; i < applicationUsers.Length; i++)
        {
            baskets[i].ApplicationUserId = applicationUsers[i].Id;
            adressess[i].ApplicationUserId = applicationUsers[i].Id;
            orders[i].ApplicationUserId = applicationUsers[i].Id;
        }




        modelBuilder.Entity<ApplicationUser>().HasData(applicationUsers);
        modelBuilder.Entity<Adress>().HasData(adressess);
        modelBuilder.Entity<CatalogItem>().HasData(catalogItems);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<Basket>().HasData(baskets);
        modelBuilder.Entity<OrderItem>().HasData(orderItems);
        modelBuilder.Entity<BasketItem>().HasData(basketItems);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        seedDate(modelBuilder);
    }

}




