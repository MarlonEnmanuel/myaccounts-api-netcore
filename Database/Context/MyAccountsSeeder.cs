using Microsoft.EntityFrameworkCore;
using MyAccounts.Database.Models;

namespace MyAccounts.Database.Context
{
    public class MyAccountsSeeder
    {
        public static void Seed (ModelBuilder modelBuilder)
        {
            // seed usuarios
            modelBuilder.Entity<User>().HasData(new[]
            {
                new User (key: "73671450") { Id = 1 },
                new User (key: "46734866") { Id = 2 },
            });

            // seed personas
            modelBuilder.Entity<Person>().HasData(new[]
            {
                new Person ("Marlon", 1, true, true) { Id = 1 },
                new Person ("Lucia" , 2, true, true) { Id = 2 },
            });

            // seed tarjetas debito
            modelBuilder.Entity<Card>().HasData(new[]
            {
                new Card (name: "Efectivo", personId: 1 ) { Id = 1 },
                new Card (name: "BCP",      personId: 1 ) { Id = 2 },
                new Card (name: "Banbif",   personId: 1 ) { Id = 3 },
                new Card (name: "Efectivo", personId: 2 ) { Id = 4 },
                new Card (name: "BCP",      personId: 2 ) { Id = 5 },
                new Card (name: "Banbif",   personId: 2 ) { Id = 6 },
            });

            // seed tarjetas crédito
            modelBuilder.Entity<Card>().HasData(new[]
            {
                new Card (name: "OH",   personId: 1, cutDay: 8,  paymentDay: 5  ) { Id = 7 },
                new Card (name: "BBVA", personId: 1, cutDay: 20, paymentDay: 16 ) { Id = 8 },
                new Card (name: "CMR",  personId: 2, cutDay: 10, paymentDay: 5  ) { Id = 9 },
            });
        }
    }
}
