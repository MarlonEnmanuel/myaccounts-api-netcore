namespace MyAccounts.Api.Modules.General.Dtos
{
    public class AuthDataDto
    {
        public UserAuthDto User { get; set; } = default!;

        public List<PersonAuthDto> Persons { get; set; } = default!;

        public List<CardAuthDto> Cards { get; set; } = default!;
    }
}
