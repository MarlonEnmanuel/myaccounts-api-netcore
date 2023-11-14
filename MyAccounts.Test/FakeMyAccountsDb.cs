using MyAccounts.Api.Database.Models;

namespace MyAccounts.Test
{
    public class FakeMyAccountsDb
    {
        public readonly List<User> Users = new();
        public readonly List<Person> Persons = new();
        public readonly List<Card> Cards = new();
        public readonly List<Payment> Payments = new();
        public readonly List<PaymentSplit> PaymentSplits = new();
    }
}
