namespace MyAccounts.Dtos
{
    public class InitialDataDto
    {
        public UserDto User { get; set; } = default!;

        public ICollection<PersonDto> Persons { get; set; } = default!;

        public ICollection<CardDto> Cards { get; set; } = default!;
    }
}
