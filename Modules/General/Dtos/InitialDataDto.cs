using MyAccounts.Database.Models;

namespace MyAccounts.Dtos
{
    public class InitialDataDto
    {
        public ICollection<Person> Persons { get; set; } = default!;
        public ICollection<Card> Cards { get; set; } = default!;
    }
}
