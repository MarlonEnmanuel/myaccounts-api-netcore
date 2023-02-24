using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Config;
using MyAccounts.Database.Interfaces;
using MyAccounts.Database.Models;
using MyAccounts.Modules.Security;

namespace MyAccounts.Database.Context
{
    public class MyAccountsContext : DbContext
    {
        private readonly IPrincipalService _principal;

        public MyAccountsContext(DbContextOptions options, IPrincipalService principal) : base(options)
        {
            _principal = principal;
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Card> Cards { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentSplit> PaymentSplits { get; set; } = default!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // map DateOnly type
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // prevent delete cascade
            modelBuilder.Model.GetEntityTypes()
                                .Where(e => !e.IsOwned())
                                .SelectMany(e => e.GetForeignKeys())
                                .ToList()
                                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.NoAction);

            // seeder
            MyAccountsSeeder.Seed(modelBuilder);
        }

        public override int SaveChanges()
        {
            HandleAuditables();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            HandleAuditables();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleAuditables();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleAuditables();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void HandleAuditables()
        {
            ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable && e.State == EntityState.Added)
                .ToList()
                .ForEach(e => {
                    (e.Entity as IAuditable)!.CreatedBy = _principal.UserId;
                    (e.Entity as IAuditable)!.UpdatedBy = _principal.UserId;
                    (e.Entity as IAuditable)!.CreatedDate = _principal.RequestDate;
                    (e.Entity as IAuditable)!.UpdatedDate = _principal.RequestDate;
                });
            
            ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable && e.State == EntityState.Modified)
                .ToList()
                .ForEach(e => {
                    e.Property(nameof(IAuditable.CreatedBy)).IsModified = false;
                    e.Property(nameof(IAuditable.CreatedDate)).IsModified = false;
                    (e.Entity as IAuditable)!.UpdatedBy = _principal.UserId;
                    (e.Entity as IAuditable)!.UpdatedDate = _principal.RequestDate;
                });
        }
    }
}