namespace MyAccounts.Dtos
{
    public class InitialDataDto
    {
        public ICollection<PersonDto> Persons { get; set; } = default!;
        public ICollection<CardDto> Cards { get; set; } = default!;
    }
}
