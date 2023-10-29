namespace MyAccounts.Api.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int UserId { get; set; }

        public bool IsUser { get; set; }
    }
}
