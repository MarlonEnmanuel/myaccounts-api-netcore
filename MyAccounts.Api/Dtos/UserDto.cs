namespace MyAccounts.Api.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int PersonId { get; set; } = default!;
    }
}
