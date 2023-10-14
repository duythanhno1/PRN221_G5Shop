using DAL.Config;
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL {
    public class FRMDbContext : IdentityDbContext<User> {
        public FRMDbContext() {

        }

        public FRMDbContext(DbContextOptions<FRMDbContext> options) : base(options) {
        }


        public DbSet<Category> Category { get; set; }
        public DbSet<User> Customer { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Transaction> TransactionShared { get; set; }
        public DbSet<Comment> Comment { get; set; }

        public DbSet<Cart> Cart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                IConfiguration config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json").Build();

                string connectionString = config["ConnectionStrings:DefaultConnection"];
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new RoleClaimConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserClaimConfig());
            modelBuilder.ApplyConfiguration(new UserLoginConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new UserTokenConfig());

            #region Orders
            modelBuilder.Entity<Orders>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            #endregion
            #region Transaction
            modelBuilder.Entity<Transaction>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            #endregion

            #region Cart
            modelBuilder.Entity<Cart>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            #endregion

            //configuration for relationship entitu
            modelBuilder.Entity<User>()
                .HasMany(m => m.Orders)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
