using Microsoft.EntityFrameworkCore;
    public class ApplicationContext : DbContext
    {
        public DbSet<Adress> Adress { get; set; }

        public DbSet<RefreshToken> RefreshToken { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Basket> Basket { get; set; }

        public DbSet<Order> ShopOrder { get; set; }

        public DbSet<BasketItem> BasketItem { get; set; }

        public DbSet<OrderItem> ShopOrderItem { get; set; }

        public DbSet<CatalogItem> CatalogItem { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseSqlServer("Data Source=BATTLER-PC\\MSSQLSTUDYWORK;Initial Catalog=Pizza;Integrated Security=True");
        }
    }


