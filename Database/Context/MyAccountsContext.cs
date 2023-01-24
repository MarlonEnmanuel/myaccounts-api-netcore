using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Config;
using MyAccounts.Database.Models;

namespace MyAccounts.Database.Context
{
    public class MyAccountsContext : DbContext
    {
        public MyAccountsContext(DbContextOptions options) : base(options) { }

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
    }
}