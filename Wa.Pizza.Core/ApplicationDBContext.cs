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

    public ApplicationDbContext() : this(
           new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(
                new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetConnectionString("Default")
               )
            .Options
        )
    { 

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.orderItem)
            .WithMany(oi => oi.Orders)
            .HasForeignKey(o => o.OrderItemId);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.applicationUser)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.ApplicationUserId);
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    //    optionsBuilder.UseSqlServer();
    //}
}




