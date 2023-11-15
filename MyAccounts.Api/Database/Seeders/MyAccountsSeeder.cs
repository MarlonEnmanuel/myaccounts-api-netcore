using Microsoft.EntityFrameworkCore;
using MyAccounts.Api.Database.Enums;
using MyAccounts.Api.Database.Models;

namespace MyAccounts.Api.Database.Context
{
    public class MyAccountsSeeder
    {
        public static void Seed (ModelBuilder modelBuilder)
        {
            // seed usuarios
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new() { Id = 1, Username = "mmontalvo", Password = "73671450" },
                new() { Id = 2, Username = "llorenzo", Password = "46734866" },
            });

            // seed personas
            modelBuilder.Entity<Person>().HasData(new Person[]
            {
                new() { Id = 1, UserId = 1, Name = "Marlon", IsShared = true },
                new() { Id = 2, UserId = 2, Name = "Lucía", IsShared = true },
            });

            // seed tarjetas
            modelBuilder.Entity<Card>().HasData(new Card[]
            {
                new() { Id = 1, PersonId = 1, Type = PaymentType.Debit, Name = "Efectivo" },
                new() { Id = 2, PersonId = 1, Type = PaymentType.Debit, Name = "BCP" },
                new() { Id = 3, PersonId = 1, Type = PaymentType.Debit, Name = "Banbif" },
                new() { Id = 7, PersonId = 1, Type = PaymentType.Credit, Name = "OH", CutDay = 8, PaymentDay = 5 },
                new() { Id = 8, PersonId = 1, Type = PaymentType.Credit, Name = "BBVA", CutDay = 20, PaymentDay = 16 },

                new() { Id = 4, PersonId = 2, Type = PaymentType.Debit, Name = "Efectivo" },
                new() { Id = 5, PersonId = 2, Type = PaymentType.Debit, Name = "BCP" },
                new() { Id = 6, PersonId = 2, Type = PaymentType.Debit, Name = "Banbif" },
                new() { Id = 9, PersonId = 2, Type = PaymentType.Credit, Name = "CMR", CutDay = 10, PaymentDay = 5 },
            });
        }
    }
}
