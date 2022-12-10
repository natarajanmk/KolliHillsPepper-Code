using KH.Pepper.Core.Domain;
using KH.Pepper.Infra.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace KH.Pepper.Infra.DataBase
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public DbSet<AddToCart> AddToCarts => Set<AddToCart>();
        public DbSet<ContactUs> Contactus => Set<ContactUs>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<ProductQuantity> ProductQtys => Set<ProductQuantity>();
        public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
        public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
        public DbSet<AdminUser> AdminUser => Set<AdminUser>();
        public DbSet<ProductOrderDetails> ProductOrderDetails => Set<ProductOrderDetails>();


        //private readonly IUserSessionProvider userSessionProvider;

        public AppDbContext()
        {

        }

        //inject UserSession Provider
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            var options = new SqlServerDbContextOptionsBuilder(builder);
            options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            options.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_CI_AS");
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductTypeConfiguration).Assembly);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);

            builder.Properties<decimal?>().HavePrecision(23, 11);
        }

        public async override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();

            //var session = await _userSessionProvider.GetUserSession(cancellationToken);

            ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Added)
                .Select(t => t.Entity)
                .OfType<BaseEntity>()
                .ToList()
                .ForEach(entity =>
                {
                    entity.CreatedOn = SystemClock.Now;
                    entity.LastChangedOn = SystemClock.Now;
                    //apply user session here..
                });

            ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Modified)
                .Select(t => t.Entity)
                .OfType<BaseEntity>()
                .ToList()
                .ForEach(entity =>
                {
                    entity.LastChangedOn = SystemClock.Now;
                });

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
