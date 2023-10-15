namespace MyAccounts.Dtos
{
    public class InitialDataDto
    {
        public UserDto LoguedUser { get; set; } = default!;

        public ICollection<PersonDto> Persons { get; set; } = default!;

        public ICollection<CardDto> Cards { get; set; } = default!;
    }
}
