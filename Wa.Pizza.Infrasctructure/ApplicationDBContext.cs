using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ApplicationDbContext : DbContext
    {
    public DbSet<Adress> Adress { get; set; }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }

    public DbSet<Basket> Basket { get; set; }

    public DbSet<Order> ShopOrder { get; set; }

    public DbSet<BasketItem> BasketItem { get; set; }

    public DbSet<OrderItem> ShopOrderItem { get; set; }

    public DbSet<CatalogItem> CatalogItem { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){
            
    }

    public ApplicationDbContext() 
    { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        Adress[] adressess = new Adress[]
            {
                new Adress { Id = -1, AdressString = "Corusant 19" },
                new Adress { Id = -2, AdressString = "Omega-4"},
                new Adress { Id = -3, AdressString = "Terra-4"}
            };

        modelBuilder.Entity<Adress>().HasData(adressess);

        Order[] orders = new Order[]
            {
                new Order { Id = -66, CreationDate = new DateTime(2019, 12, 31), Description = "The republic will be reogranised into a first galactic empire", Status = OrderStatus.Delivered },
                new Order { Id = -1, CreationDate = new DateTime(2186, 10, 11), Description = "Bring extra tomato sauce, don't be late, don't make Aria mad", Status = OrderStatus.Delivering},
                new Order { Id = -2, CreationDate = new DateTime(4000, 1, 12), Description = "Someone order pepperoni pizza into the Emperor's palace", Status = OrderStatus.Canceled}

            };
        OrderItem[] orderItems = new OrderItem[]
        {
            new OrderItem { Id = -1, Discount = 1, Quantity = 1, UnitPrice = 10000, OrderId = orders[0].Id, Order = orders[0] },
            new OrderItem { Id = -2, Discount = 1, Quantity = 1, UnitPrice = 10000, OrderId = orders[0].Id, Order = orders[0]},
            new OrderItem { Id = -3, Discount = 0.8M, Quantity = 3000000, UnitPrice = 4000, OrderId = orders[0].Id, Order = orders[0]},
            new OrderItem { Id = -4, Discount = 0.01M, Quantity = 50, UnitPrice = 100, OrderId = orders[1].Id, Order =  orders[1]},
            new OrderItem { Id = -5, Discount = 0.99M, Quantity = 5, UnitPrice = 150, OrderId = orders[2].Id, Order =  orders[2] },

        };
        orders[0].OrderItems = new List<OrderItem>() { orderItems[0], orderItems[1], orderItems[2] };
        orders[1].OrderItems = new List<OrderItem>() { orderItems[2] };
        orders[2].OrderItems = new List<OrderItem>() { orderItems[3] };

        CatalogItem[] catalogItems = new CatalogItem[]
        {
            new CatalogItem {  Id = -7567,  Quantity = 1, Name = "Rex", Description = "Clone trooper commander", Price = 10000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets, OrderItems =  new List<OrderItem> {orderItems[0] } },
            new CatalogItem {  Id = -2224,  Quantity = 1, Name = "Cody", Description = "Clone trooper commander", Price = 10000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets, OrderItems =  new List<OrderItem> {orderItems[1] } },
            new CatalogItem {  Id = -1,  Quantity = 3000000, Name = "Clone trooper", Description = "Regular clone trooper", Price = 4000, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Sweets, OrderItems =  new List<OrderItem> {orderItems[2] } },
            new CatalogItem {  Id = -2,  Quantity = 500, Name = "Tomato pizza", Description = "With extra Tomato Sauce", Price = 100, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Pizza, OrderItems =  new List<OrderItem> {orderItems[3] } },
            new CatalogItem {  Id = -3,  Quantity = 150, Name = "Pepperoni", Description = "Classic", Price = 150, CatalogType =  WA.Pizza.Core.CatalogType.CatalogType.Pizza, OrderItems =  new List<OrderItem> {orderItems[4] } },

        };

        for (int i = 0; i < orderItems.Length; i++)
        {
            orderItems[i].CatalogItem = catalogItems[i];
            orderItems[i].CatalogItemId = catalogItems[i].Id;
            orderItems[i].CatalogItemName = catalogItems[i].Name;
        }

        BasketItem[] basketItems = new BasketItem[]
        {
            new BasketItem { Id = -1, Quantity = 1, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 10000, CatalogItemName = "Rex", CatalogItem = catalogItems[0], CatalogItemId = catalogItems[0].Id,  },
            new BasketItem { Id = -2, Quantity = 1, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 10000, CatalogItemName = "Cody", CatalogItem = catalogItems[1], CatalogItemId = catalogItems[1].Id },
            new BasketItem { Id = -3, Quantity = 3000000, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Sweets, UnitPrice = 4000, CatalogItemName = "Clone trooper", CatalogItem = catalogItems[2], CatalogItemId = catalogItems[2].Id },
            new BasketItem { Id = -4, Quantity = 500, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza, UnitPrice = 100, CatalogItemName = "Tomato pizza", CatalogItem = catalogItems[3], CatalogItemId = catalogItems[3].Id },
            new BasketItem { Id = -5, Quantity = 150, CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza, UnitPrice = 150, CatalogItemName = "Classic", CatalogItem = catalogItems[4], CatalogItemId = catalogItems[4].Id },
        };

        catalogItems[0].BasketItems = new List<BasketItem>() { basketItems[0] };
        catalogItems[1].BasketItems = new List<BasketItem>() { basketItems[1] };
        catalogItems[2].BasketItems = new List<BasketItem>() { basketItems[2] };
        catalogItems[3].BasketItems = new List<BasketItem>() { basketItems[3] };
        catalogItems[4].BasketItems = new List<BasketItem>() { basketItems[4] };

        Basket[] baskets = new Basket[]
        {
            new Basket { Id = -1, LastModified = new DateTime(2050,12,10), BasketItems = new List<BasketItem> { basketItems[0], basketItems[1], basketItems[2] } },
            new Basket { Id = -2, LastModified = new DateTime(2186, 10, 11),BasketItems = new List<BasketItem> {  basketItems[3] } },
            new Basket { Id = -3, LastModified = new DateTime(4000, 1, 13), BasketItems = new List<BasketItem> { basketItems[4] } },
        };

        basketItems[0].Basket = baskets[0];
        basketItems[1].Basket = baskets[0];
        basketItems[2].Basket = baskets[0];
        basketItems[3].Basket = baskets[1];
        basketItems[4].Basket = baskets[2];

        ApplicationUser[] applicationUsers = new ApplicationUser[]
        {
            /*new ApplicationUser { Id = -1, AdressID = adressess[0].Id, Adress = adressess[0], Basket = baskets[0], Orders = new List<Order> { orders[0], orders[1], orders[2]} },
            new ApplicationUser { Id = -2, AdressID = adressess[1].Id, Adress = adressess[1], Basket = baskets[1], Orders = new List<Order> { orders[3]}},
            new ApplicationUser { Id = -3, AdressID = adressess[2].Id, Adress = adressess[2], Basket = baskets[2], Orders = new List<Order> { orders[4]}}*/
        };

 /*       for (int i = 0; i < orderItems.Length; i++)
        {
            baskets[i].ApplicationUser = applicationUsers[i];
            baskets[i].ApplicationUserId = applicationUsers[i].Id;

            adressess[i].ApplicationUser = applicationUsers[i];

            orders[i].ApplicationUser = applicationUsers[i];
            orders[i].ApplicationUserId = applicationUsers[i].Id;
        }*/





        modelBuilder.Entity<Adress>().HasData(adressess);
/*        modelBuilder.Entity<ApplicationUser>().HasData(applicationUsers);
        modelBuilder.Entity<Basket>().HasData(baskets);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<OrderItem>().HasData(orders);
        modelBuilder.Entity<BasketItem>().HasData(basketItems);
        modelBuilder.Entity<CatalogItem>().HasData(catalogItems);*/


        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.ApplicationUser)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.ApplicationUserId);
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.CatalogItem)
            .WithMany(c => c.OrderItems);

        modelBuilder.Entity<Basket>()
            .HasMany(b => b.BasketItems)
            .WithOne(bi => bi.Basket);
        modelBuilder.Entity<Basket>()
            .HasOne(b => b.ApplicationUser)
            .WithOne(au => au.Basket)
            .HasForeignKey<Basket>(b => b.ApplicationUserId);

        modelBuilder.Entity<BasketItem>()
            .HasOne(bi => bi.CatalogItem)
            .WithMany(ci => ci.BasketItems)
            .HasForeignKey(bi => bi.CatalogItemId);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(au => au.Adress)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<ApplicationUser>(au => au.AdressID);

           

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    //    optionsBuilder.UseSqlServer();
    //}
}




