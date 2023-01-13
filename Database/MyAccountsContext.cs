using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Config;
using MyAccounts.Database.Entities;

namespace MyAccounts.Database
{
    public class MyAccountsContext : DbContext
    {
        public MyAccountsContext(DbContextOptions options) : base(options) {}


        public DbSet<Card> Cards { get; set; } = default!;
        public DbSet<DebitCard> DebitCards { get; set; } = default!;
        public DbSet<CreditCard> CreditCards { get; set; } = default!;

        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<DebitPayment> DebitPayments { get; set; } = default!;
        public DbSet<CreditPayment> CreditPayments { get; set; } = default!;

        public DbSet<PaymentSplit> PaymentSplits { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;


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

            // seeder personas
            modelBuilder.Entity<Person>().HasData(new[]
            {
                new Person { Id = 1, Name = "Marlon" },
                new Person { Id = 2, Name = "Lucía" },
            });

            // seeder usuarios
            modelBuilder.Entity<User>().HasData(new[]
            {
                new User { Id = 1, Key = "73671450", PersonId = 1 },
                new User { Id = 2, Key = "46734866", PersonId = 2 },
            });

            // seeder tarjetas debito
            modelBuilder.Entity<DebitCard>().HasData(new[]
            {
                // Marlon
                new DebitCard { Id = 1, Name = "Efectivo", PersonId = 1 },
                new DebitCard { Id = 2, Name = "BCP",      PersonId = 1 },
                new DebitCard { Id = 3, Name = "Banbif",   PersonId = 1 },
                // Lucía
                new DebitCard { Id = 4, Name = "Efectivo", PersonId = 2 },
                new DebitCard { Id = 5, Name = "BCP",      PersonId = 2 },
                new DebitCard { Id = 6, Name = "Banbif",   PersonId = 2 },
            });

            // seeder tarjetas crédito
            modelBuilder.Entity<CreditCard>().HasData(new[]
            {
                // Marlon
                new CreditCard { Id = 7, Name = "OH", PersonId = 1, CutDay = 8, PaymentDay = 5 },
                new CreditCard { Id = 8, Name = "BBVA", PersonId = 1, CutDay = 20, PaymentDay = 16 },
                // Lucía
                new CreditCard { Id = 9, Name = "CMR", PersonId = 2, CutDay = 10, PaymentDay = 5 },
            });
        }
    }
}