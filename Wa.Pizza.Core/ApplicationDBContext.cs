using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        //    optionsBuilder.UseSqlServer();
        //}
    }




