using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Config;
using MyAccounts.Database.Models;

namespace MyAccounts.Database
{
    public class MyAccountsContext : DbContext
    {
        public MyAccountsContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // prevent delete cascade
            modelBuilder.Model.GetEntityTypes()
                                .Where(e => !e.IsOwned())
                                .SelectMany(e => e.GetForeignKeys())
                                .ToList()
                                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.NoAction);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // map DateOnly type
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        public DbSet<Card> Cards { get; set; } = default!;
        public DbSet<DebitCard> DebitCards { get; set; } = default!;
        public DbSet<CreditCard> Credits { get; set; } = default!;

        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<DebitPayment> DebitPayments { get; set; } = default!;
        public DbSet<CreditPayment> CreditPayments { get; set; } = default!;

        public DbSet<PaymentSplit> PaymentSplits { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
    }
}