using MyAccounts.Api.Database.Enums;

namespace MyAccounts.Api.Modules.General.Dtos
{
    public class CardAuthDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PersonName { get; set; } = string.Empty;

        public PaymentType Type { get; set; }

        public string TypeName { get; set; } = string.Empty;
    }
}
